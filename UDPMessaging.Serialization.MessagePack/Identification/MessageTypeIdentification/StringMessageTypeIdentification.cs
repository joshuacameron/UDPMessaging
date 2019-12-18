using MessagePack;
using UDPMessaging.Identification.MessageTypeIdentification;

namespace UDPMessaging.Serialization.MessagePack.Identification.MessageTypeIdentification
{
    [MessagePackObject]
    public class StringMessageTypeIdentification : IMessageTypeIdentification
    {
        [Key(0)]
        public string Identification { get; }

        public StringMessageTypeIdentification(string identification)
        {
            Identification = identification;
        }

        public override bool Equals(object obj)
        {
            switch (obj)
            {
                case string s:
                    return s == Identification;
                case StringMessageTypeIdentification spi:
                    return spi.Identification == Identification;
            }

            return false;
        }

        public bool Equals(IMessageTypeIdentification obj)
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
