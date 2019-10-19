using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Threading;
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
        private CountdownEvent _awaitingMessage;

        [TestMethod]
        public void IntTest()
        {
            _awaitingMessage = new CountdownEvent(2);

            const string peerAName = "PeerA";
            const int peerAPort = 4000;

            const string peerBName = "PeerB";
            const int peerBPort = 5000;

            IPeerIdentification peerAIdentification = new StringPeerIdentification(peerAName);
            IPeerIdentification peerBIdentification = new StringPeerIdentification(peerBName);

            IPeerManager peerApeerManager = new PeerManager();
            peerApeerManager.AddOrUpdatePeer(peerBIdentification, new IPEndPoint(IPAddress.Loopback, peerBPort));

            IPeerManager peerBpeerManager = new PeerManager();
            peerBpeerManager.AddOrUpdatePeer(peerAIdentification, new IPEndPoint(IPAddress.Loopback, peerAPort));

            ISerializer serializer = new JSONSerialiser();

            IUDPNetworking peerA = new UDPNetworking(
                peerAIdentification,
                peerApeerManager,
                serializer,
                new IPEndPoint(IPAddress.Loopback, peerAPort)
            );

            IUDPNetworking peerB = new UDPNetworking(
                peerBIdentification,
                peerBpeerManager,
                serializer,
                new IPEndPoint(IPAddress.Loopback, peerBPort)
            );

            peerA.OnMessageReceived += OnMessageReceived;
            peerB.OnMessageReceived += OnMessageReceived;

            peerA.SendMessageAsync(new StringMessage()
            {
                To = peerBIdentification,
                From = peerAIdentification,
                Data = "Hello B!"
            });

            peerB.SendMessageAsync(new StringMessage()
            {
                To = peerAIdentification,
                From = peerBIdentification,
                Data = "Hello A!"
            });

            _awaitingMessage.Wait();

            peerA.Dispose();
            peerB.Dispose();
        }

        private void OnMessageReceived(object sender, IBaseMessage message)
        {
            Console.WriteLine(((StringMessage)message).Data);

            _awaitingMessage.Signal();
        }
    }
}
