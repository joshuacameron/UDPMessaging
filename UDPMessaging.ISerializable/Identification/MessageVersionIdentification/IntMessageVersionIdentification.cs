using System;
using System.Runtime.Serialization;
using UDPMessaging.Extensions;

namespace UDPMessaging.Identification.MessageVersionIdentification
{
    [Serializable]
    public class IntMessageVersionIdentification : IMessageVersionIdentification, ISerializable
    {
        private readonly int _identification;

        public IntMessageVersionIdentification(int identification)
        {
            _identification = identification;
        }

        protected IntMessageVersionIdentification(SerializationInfo info, StreamingContext ctxt)
        {
            _identification = info.GetValue<int>(nameof(_identification));
        }

        public override bool Equals(object obj)
        {
            return CompareTo(obj) == 0;
        }

        public override int GetHashCode()
        {
            return _identification;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(_identification), _identification);
        }

        public int CompareTo(object obj)
        {
            switch (obj)
            {
                case int i:
                    return _identification.CompareTo(i);
                case IntMessageVersionIdentification imvi:
                    return _identification.CompareTo(imvi._identification);
            }

            throw new ArgumentException("obj is not a compatible type with this instance");
        }

        public object GetIdentification()
        {
            return _identification;
        }

        public bool Equals(IMessageVersionIdentification obj)
        {
            return Equals((object)obj);
        }

        public int CompareTo(IMessageVersionIdentification obj)
        {
            return CompareTo((object)obj);
        }
    }
}
