using System;
using MessagePack;

namespace UDPMessaging.Serialization.MessagePack.Identification.MessageTypeIdentification
{
    [MessagePackObject]
    public class TypeMessageTypeIdentification : StringMessageTypeIdentification
    {
        public TypeMessageTypeIdentification(Type type)
            : base(type.FullName) { }
    }
}
