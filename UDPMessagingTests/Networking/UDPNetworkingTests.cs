using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UDPMessaging.Identification.MessageVersionIdentification;
using UDPMessaging.Identification.PeerIdentification;
using UDPMessaging.Messages;
using UDPMessaging.Serialisation;

namespace UDPMessagingTests.Networking
{
    [TestClass]
    public class UDPNetworkingTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            string peerAName = Guid.NewGuid().ToString();
            string peerBName = Guid.NewGuid().ToString();

            string peerAData = Guid.NewGuid().ToString();
            string peerBData = Guid.NewGuid().ToString();

            StringMessage message = new StringMessage()
            {
                To = new StringPeerIdentification(peerAName),
                From = new StringPeerIdentification(peerBName),
                Version = new IntMessageVersionIdentification(10),
                Data = peerAData
            };

            ISerializer serializer = new JSONSerialiser();

            byte[] messageBytes = serializer.Serialize(message);

            StringMessage newMessage = (StringMessage)serializer.Deserialize(messageBytes);



            //BasicUDPNetworking peerA = new BasicUDPNetworking(
            //    StringPeerIdentification.Generate(),     
            //    new PeerManager(), 
            //    new Serializer(), 
            //    new IPEndPoint(IPAddress.Any, 4000)
            //);
        }
    }
}
