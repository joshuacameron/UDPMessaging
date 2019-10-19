using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using UDPMessaging.Exceptions;

namespace UDPMessaging.Networking
{
    internal sealed class UDPClient : IUDPClient
    {
        public event EventHandler<SendFailureException> OnMessageSendFailure;
        public event EventHandler<ReceiveFailureException> OnMessageReceivedFailure;
        public event EventHandler<UdpReceiveResult> OnMessageReceived;

        private UdpClient _udpClient;
        private readonly IPEndPoint _ipEndPoint;

        private readonly Thread _listeningThread;
        private readonly ManualResetEvent _onThreadStarted;
        private readonly ManualResetEvent _onStop;

        private readonly ManualResetEvent _isSocketReady;

        
        private bool _disposedValue; // To detect redundant calls

        public UDPClient(IPEndPoint ipEndPoint)
        {
            _ipEndPoint = ipEndPoint;
            _udpClient = new UdpClient(_ipEndPoint);
            DisableIcmpUnreachable(_udpClient);

            _isSocketReady = new ManualResetEvent(true);

            _listeningThread = new Thread(Receive);
            _onThreadStarted = new ManualResetEvent(false);
            _onStop = new ManualResetEvent(false);

            _listeningThread.Start();
        }

        public async Task<bool> SendAsync(byte[] messageBytes, IPEndPoint ipEndPoint)
        {
            try
            {
                _isSocketReady.WaitOne();
                return await _udpClient.SendAsync(messageBytes, messageBytes.Length, ipEndPoint) > 0;
            }
            catch (Exception e)
            {
                OnMessageSendFailure?.Invoke(this, new SendFailureException($"Error sending message to {ipEndPoint}", e, null));
                RebuildUdpClient();
                return false;
            }
        }

        private void Receive()
        {
            Task onStopTask = Task.Run(() => _onStop.WaitOne());
            _onThreadStarted.Set();

            while (true)
            {
                try
                {
                    _isSocketReady.WaitOne();

                    Task<UdpReceiveResult> result = _udpClient.ReceiveAsync();

                    if (Task.WaitAny(onStopTask, result) == 0)
                    {
                        return;
                    }

                    OnMessageReceived?.Invoke(this, result.Result);
                }
                catch (Exception e)
                {
                    OnMessageReceivedFailure?.Invoke(this, new ReceiveFailureException("UDPClient threw an exception when receiving", e));
                    RebuildUdpClient();
                }
            }
        }

        private void RebuildUdpClient()
        {
            lock (_isSocketReady)
            {
                if (!_isSocketReady.WaitOne(0)) return; //Return if it's already rebuilding
                _isSocketReady.Reset();
            }

            _udpClient.Dispose();
            _udpClient = new UdpClient(_ipEndPoint);
            DisableIcmpUnreachable(_udpClient);
            _isSocketReady.Set();
        }

        private static void DisableIcmpUnreachable(UdpClient udpClient)
        {
            const uint iocIn = 0x80000000;
            const uint iocVendor = 0x18000000;
            const uint sioUdpConnreset = iocIn | iocVendor | 12;
            udpClient.Client.IOControl(unchecked((int)sioUdpConnreset), new[] { Convert.ToByte(false) }, null);
        }

        public void WaitForStartup()
        {
            _onThreadStarted.WaitOne();
        }

        public void Dispose()
        {
            if (_disposedValue) return;

            _onStop.Set();
            _listeningThread.Join();
            _udpClient?.Dispose();
            _isSocketReady?.Dispose();

            _disposedValue = true;
        }
    }
}
