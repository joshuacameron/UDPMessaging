using System;
using System.Runtime.Serialization;
using UDPMessaging.Extensions;

namespace UDPMessaging.Identification.PeerIdentification
{
    [Serializable]
    public class StringPeerIdentification : IPeerIdentification, ISerializable
    {
        private readonly string _identification;

        public StringPeerIdentification(string identifier)
        {
            _identification = identifier;
        }

        protected StringPeerIdentification(SerializationInfo info, StreamingContext ctxt)
        {
            _identification = info.GetValue<string>(nameof(_identification));
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
                    return s == _identification;
                case StringPeerIdentification spi:
                    return spi._identification == _identification;
            }

            return false;
        }

        public bool Equals(IPeerIdentification obj)
        {
            return Equals((object)obj);
        }

        public override int GetHashCode()
        {
            return _identification.GetHashCode();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(_identification), _identification);
        }

        public object GetIdentification()
        {
            return _identification;
        }
    }
}
