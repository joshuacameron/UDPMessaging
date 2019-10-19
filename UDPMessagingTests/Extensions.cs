using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using UDPMessaging.Identification.PeerIdentification;

namespace UDPMessagingTests
{
    public static class Extensions
    {
        private static readonly Faker Faker = new Faker();

        public static IPeerIdentification PeerIdentification(this Bogus.Randomizer randomizer)
        {
            return StringPeerIdentification.Generate();
        }
    }
}
