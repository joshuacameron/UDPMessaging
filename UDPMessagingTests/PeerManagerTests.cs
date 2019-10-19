using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UDPMessaging.Identification.PeerIdentification;
using UDPMessaging.PeerManagement;

namespace UDPMessagingTests
{
    [TestClass]
    public class PeerManagerTests
    {
        private readonly Faker _faker = new Faker();

        [TestMethod]
        public void TestMethodFaker()
        {
            IPeerManager peerManagement = new PeerManager();

            IPeerIdentification peerIdentification = _faker.Random.PeerIdentification();
            IPEndPoint ipEndPoint = _faker.Internet.IpEndPoint();



            //Add
            peerManagement.AddOrUpdatePeer(_faker.Random.PeerIdentification(), _faker.Internet.IpEndPoint());
            peerManagement.AddOrUpdatePeer(_faker.Random.PeerIdentification(), _faker.Internet.IpEndPoint());
            peerManagement.AddOrUpdatePeer(peerIdentification, _faker.Internet.IpEndPoint());

            //Update
            peerManagement.AddOrUpdatePeer(peerIdentification, ipEndPoint);

            IPEndPoint result = peerManagement[peerIdentification];
        }
    }
}
