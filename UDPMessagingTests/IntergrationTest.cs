using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UDPMessaging.Identification.PeerIdentification;
using UDPMessaging.Messages;
using UDPMessaging.Networking;
using UDPMessaging.PeerManagement;
using UDPMessaging.Serialisation;

namespace UDPMessagingTests
{
    [TestClass]
    public class IntergrationTest
    {
        [TestMethod]
        public void IntTest()
        {
            const string peerAName = "PeerA";
            const int peerAPort = 4000;

            const string peerBName = "PeerB";
            const int peerBPort = 5000;


            IPeerIdentification peerAIdentification = new StringPeerIdentification(peerAName);
            IPeerIdentification peerBIdentification = new StringPeerIdentification(peerBName);

            IPeerManager peerApeerManager = new PeerManager();
            peerApeerManager.AddOrUpdatePeer(peerBIdentification, new IPEndPoint(IPAddress.Loopback, peerBPort));

            IPeerManager peerBpeerManager = new PeerManager();
            peerApeerManager.AddOrUpdatePeer(peerAIdentification, new IPEndPoint(IPAddress.Loopback, peerAPort));

            ISerializer serializer = new JSONSerialiser();

            //IPeerIdentification peerName, IPeerManager peerManager, ISerializer serializer, IPEndPoint ipEndPoint
            IUDPNetworking peerA = new BasicUDPNetworking(
                peerAIdentification,
                peerApeerManager,
                serializer,
                new IPEndPoint(IPAddress.Loopback, peerAPort)
            );

            IUDPNetworking peerB = new BasicUDPNetworking(
                peerBIdentification,
                peerBpeerManager,
                serializer,
                new IPEndPoint(IPAddress.Loopback, peerBPort)
            );

            peerB.OnMessageReceived += PeerB_OnMessageReceived;

            Thread.Sleep(1000);

            peerA.SendMessageAsync(new StringMessage()
            {
                To = peerBIdentification,
                From = peerAIdentification,
                Data = "Hello world!"
            });

            Thread.Sleep(1000);

            peerA.Dispose();
            peerB.Dispose();
        }

        private void PeerB_OnMessageReceived(object sender, IBaseMessage message)
        {
            StringMessage stringMessage = message as StringMessage;

            Console.WriteLine(stringMessage.Data);
        }
    }
}
