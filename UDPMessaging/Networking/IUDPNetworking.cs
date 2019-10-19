using System;
using System.Net;
using UDPMessaging.Exceptions;
using UDPMessaging.Identification.PeerIdentification;
using UDPMessaging.Messages;
using UDPMessaging.PeerManagement;

namespace UDPMessaging.Networking
{
    public interface IUDPNetworking : IDisposable
    {
        void SendMessageAsync(IBaseMessage message);
        event EventHandler<IBaseMessage> OnMessageReceived;
        event EventHandler<ReceiveFailureException> OnMessageReceivedFailure;
        event EventHandler<SendFailureException> OnMessageSendFailure;
        event EventHandler<IPeerIdentification> OnNewConnectedPeer;

        IPeerManager PeerManager { get; }
        IPeerIdentification PeerName { get; }
    }
}
