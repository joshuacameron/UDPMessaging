using System;
using System.Runtime.Serialization;

namespace UDPMessaging.Identification.MessageTypeIdentification
{
    [Serializable()]
    public class TypeMessageTypeIdentification : StringMessageTypeIdentification
    {
        public TypeMessageTypeIdentification(Type type)
            :base(type.FullName) { }

        protected TypeMessageTypeIdentification(SerializationInfo info, StreamingContext ctxt)
            : base(info, ctxt) { }
    }
}
