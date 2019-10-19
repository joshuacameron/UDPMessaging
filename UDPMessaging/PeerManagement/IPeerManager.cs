using System.Net;
using UDPMessaging.Identification.PeerIdentification;

namespace UDPMessaging.PeerManagement
{
    public interface IPeerManager
    {
        bool AddOrUpdatePeer(IPeerIdentification peerIdentification, IPEndPoint ipEndPoint);
        IPEndPoint GetPeerIPEndPoint(IPeerIdentification peerIdentification);
        IPEndPoint this[IPeerIdentification peerIdentification] { get; }
    }
}
