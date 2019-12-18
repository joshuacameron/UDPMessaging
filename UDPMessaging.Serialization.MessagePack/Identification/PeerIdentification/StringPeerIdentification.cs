using System;
using MessagePack;
using UDPMessaging.Identification.PeerIdentification;

namespace UDPMessaging.Serialization.MessagePack.Identification.PeerIdentification
{
    [MessagePackObject]
    public class StringPeerIdentification : IPeerIdentification
    {
        [Key(0)]
        public string Identification { get; }

        public StringPeerIdentification(string identifier)
        {
            Identification = identifier;
        }

        public static StringPeerIdentification Generate()
        {
            return new StringPeerIdentification(Guid.NewGuid().ToString());
        }

        public override bool Equals(object obj)
        {
            switch (obj)
            {
                case string s:
                    return s == Identification;
                case StringPeerIdentification spi:
                    return spi.Identification == Identification;
            }

            return false;
        }

        public bool Equals(IPeerIdentification obj)
        {
            return Equals((object)obj);
        }

        public override int GetHashCode()
        {
            return Identification.GetHashCode();
        }

        public object GetIdentification()
        {
            return Identification;
        }
    }
}
