using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using UDPMessaging.Exceptions;

namespace UDPMessaging.Networking
{
    public interface IUDPClient : IDisposable
    {
        event EventHandler<ReceiveFailureException> OnMessageReceivedFailure;
        Task<bool> SendAsync(byte[] messageBytes, IPEndPoint ipEndPoint);
        event EventHandler<UdpReceiveResult> OnMessageReceived;
        void WaitForStartup();
    }
}