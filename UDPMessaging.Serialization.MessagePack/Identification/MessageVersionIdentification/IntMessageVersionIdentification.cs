using System;
using MessagePack;
using UDPMessaging.Identification.MessageVersionIdentification;

namespace UDPMessaging.Serialization.MessagePack.Identification.MessageVersionIdentification
{
    [MessagePackObject]
    public class IntMessageVersionIdentification : IMessageVersionIdentification
    {
        [Key(0)]
        public int Identification { get; }

        public IntMessageVersionIdentification(int identification)
        {
            Identification = identification;
        }

        public override bool Equals(object obj)
        {
            return CompareTo(obj) == 0;
        }

        public override int GetHashCode()
        {
            return Identification;
        }

        public int CompareTo(object obj)
        {
            switch (obj)
            {
                case int i:
                    return Identification.CompareTo(i);
                case IntMessageVersionIdentification imvi:
                    return Identification.CompareTo(imvi.Identification);
            }

            throw new ArgumentException("obj is not a compatible type with this instance");
        }

        public object GetIdentification()
        {
            return Identification;
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
