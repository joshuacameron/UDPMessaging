﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using UDPMessaging.Exceptions;
using UDPMessaging.Utilities;
using UDPMessaging.Utilities.ProcessingThread;

namespace UDPMessaging.Networking
{
    public class UDPClient : IUDPClient
    {
        public event EventHandler<SendFailureException> OnMessageSendFailure;
        public event EventHandler<ReceiveFailureException> OnMessageReceivedFailure;
        public event EventHandler<Tuple<byte[], IPEndPoint>> OnMessageReceived;

        protected bool DisposedValue; // To detect redundant calls

        private readonly ProcessingHandleThread _listeningThread;

        private UdpClient _udpClient;
        private readonly IPEndPoint _ipEndPoint;

        private bool _isRebuilding;
        private readonly object _isRebuildingLockObject;
        private readonly ManualResetEvent _isSocketReady;

        public UDPClient(IPEndPoint ipEndPoint)
        {
            _ipEndPoint = ipEndPoint;
            _udpClient = InitUdpClient(_udpClient, _ipEndPoint);

            _isRebuilding = false;
            _isRebuildingLockObject = new object();
            _isSocketReady = new ManualResetEvent(true);

            _listeningThread = new ProcessingHandleThread(
                new Semaphore(1, 1, Guid.NewGuid().ToString()),
                Receive,
                exception => { ReceiveException(exception); return true; }
            );

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
            try
            {
                _isSocketReady.WaitOne();

                Task<UdpReceiveResult> result = _udpClient.ReceiveAsync();
                result.Wait();

                if (result.Result != null)
                {
                    OnMessageReceived?.Invoke(this, new Tuple<byte[], IPEndPoint>(result.Result.Buffer, result.Result.RemoteEndPoint));
                }
            }
            catch (Exception e)
            {
                ReceiveException(e);
                RebuildUdpClient();
            }
        }

        private void ReceiveException(Exception e)
        {
            OnMessageReceivedFailure?.Invoke(this, new ReceiveFailureException("UDPClient threw an error when receiving", e));
        }

        private void RebuildUdpClient()
        {
            lock (_isRebuildingLockObject)
            {
                if (_isRebuilding)
                {
                    return; //It's currently rebuilding
                }
                _isRebuilding = true;
                _isSocketReady.Reset();
            }

            _udpClient = InitUdpClient(_udpClient, _ipEndPoint);

            lock (_isRebuildingLockObject)
            {
                _isRebuilding = false;
                _isSocketReady.Set();
            }
        }

        private static UdpClient InitUdpClient(UdpClient udpClient, IPEndPoint ipEndPoint)
        {
            udpClient?.Dispose();
            udpClient = null;
            udpClient = new UdpClient(ipEndPoint);
            DisableIcmpUnreachable(udpClient);
            return udpClient;
        }

        private static void DisableIcmpUnreachable(UdpClient udpClient)
        {
            const uint iocIn = 0x80000000;
            const uint iocVendor = 0x18000000;
            const uint sioUdpConnreset = iocIn | iocVendor | 12;
            udpClient.Client.IOControl(unchecked((int)sioUdpConnreset), new[] { Convert.ToByte(false) }, null);
        }

        public void Dispose()
        {
            if (DisposedValue) return;

            _listeningThread?.Dispose();
            _udpClient?.Dispose();
            _isSocketReady?.Dispose();

            DisposedValue = true;
        }
    }
}
