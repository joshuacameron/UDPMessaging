using System;
using System.Runtime.Serialization;
using UDPMessaging.Identification.MessageTypeIdentification;
using UDPMessaging.Identification.MessageVersionIdentification;
using UDPMessaging.Identification.PeerIdentification;
using UDPMessaging.Extensions;

namespace UDPMessaging.Messages
{
    [Serializable()]
    public abstract class BaseMessage : IBaseMessage, ISerializable
    {
        public IPeerIdentification To { get; set; }
        private const string ToSerialisationStr = "To";
        public IPeerIdentification From { get; set; }
        private const string FromSerialisationStr = "From";
        public IMessageTypeIdentification Type { get; protected set; }
        private const string TypeSerialisationStr = "Type";
        public IMessageVersionIdentification Version { get; set; }
        private const string VersionSerialisationStr = "Version";

        protected BaseMessage(IMessageTypeIdentification type)
        {
            Type = type;
        }

        protected BaseMessage(SerializationInfo info, StreamingContext context)
        {
            if (info == null) return;
            To = info.GetValue<IPeerIdentification>(ToSerialisationStr);
            From = info.GetValue<IPeerIdentification>(FromSerialisationStr);
            Type = info.GetValue<IMessageTypeIdentification>(TypeSerialisationStr);
            Version = info.GetValue<IMessageVersionIdentification>(VersionSerialisationStr);
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue<IPeerIdentification>(ToSerialisationStr, To);
            info.AddValue<IPeerIdentification>(FromSerialisationStr, From);
            info.AddValue<IMessageTypeIdentification>(TypeSerialisationStr, Type);
            info.AddValue<IMessageVersionIdentification>(VersionSerialisationStr, Version);
        }
    }
}
