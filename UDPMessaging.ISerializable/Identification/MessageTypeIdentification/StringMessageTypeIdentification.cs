using System;
using System.Runtime.Serialization;
using UDPMessaging.Extensions;

namespace UDPMessaging.Identification.MessageTypeIdentification
{
    [Serializable]
    public class StringMessageTypeIdentification : IMessageTypeIdentification, ISerializable
    {
        private readonly string _identification;

        public StringMessageTypeIdentification(string identification)
        {
            _identification = identification;
        }

        protected StringMessageTypeIdentification(SerializationInfo info, StreamingContext ctxt)
        {
            _identification = info.GetValue<string>(nameof(_identification));
        }

        public override bool Equals(object obj)
        {
            switch (obj)
            {
                case string s:
                    return s == _identification;
                case StringMessageTypeIdentification spi:
                    return spi._identification == _identification;
            }

            return false;
        }

        public bool Equals(IMessageTypeIdentification obj)
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
