using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using UDPMessaging.Identification.PeerIdentification;

namespace UDPMessaging.PeerManagement
{
    public class PeerManager : IPeerManager
    {
        private readonly IDictionary<IPeerIdentification, IPEndPoint> _peers;

        public PeerManager()
        {
            _peers = new ConcurrentDictionary<IPeerIdentification, IPEndPoint>();
        }

        public bool AddOrUpdatePeer(IPeerIdentification peerIdentification, IPEndPoint ipEndPoint)
        {
            _peers.TryGetValue(peerIdentification, out IPEndPoint existingValue);

            if (ipEndPoint.Equals(existingValue))
            {
                return false;
            }

            _peers[peerIdentification] = ipEndPoint;

            return existingValue == null;
        }

        public IPEndPoint GetPeerIPEndPoint(IPeerIdentification peerIdentification)
        {
            _peers.TryGetValue(peerIdentification, out IPEndPoint ipEndPoint);
            return ipEndPoint;
        }

        public IPEndPoint this[IPeerIdentification peerIdentification] => GetPeerIPEndPoint(peerIdentification);
    }
}
