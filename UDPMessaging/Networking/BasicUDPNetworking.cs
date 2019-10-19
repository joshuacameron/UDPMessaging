using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using UDPMessaging.Identification.PeerIdentification;
using UDPMessaging.PeerManagement;
using UDPMessaging.Serialisation;

namespace UDPMessaging.Networking
{
    public class BasicUDPNetworking :  BaseUDPNetworking
    {
        public BasicUDPNetworking(IPeerIdentification peerName, IPeerManager peerManager, ISerializer serializer, IPEndPoint ipEndPoint) 
            : base(peerName, peerManager, serializer, ipEndPoint) { }
    }
}
