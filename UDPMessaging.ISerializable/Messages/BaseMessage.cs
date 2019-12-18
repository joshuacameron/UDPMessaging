using System;
using System.Reflection;
using System.Runtime.Serialization;
using UDPMessaging.Identification.MessageTypeIdentification;
using UDPMessaging.Identification.MessageVersionIdentification;
using UDPMessaging.Identification.PeerIdentification;
using UDPMessaging.Extensions;

namespace UDPMessaging.Messages
{
    [Serializable]
    public abstract class BaseMessage : IBaseMessage, ISerializable
    {
        public IPeerIdentification To { get; set; }
        public IPeerIdentification From { get; set; }
        public IMessageTypeIdentification Type { get; protected set; }
        public IMessageVersionIdentification Version { get; set; }

        protected BaseMessage(IMessageTypeIdentification type)
        {
            Type = type;
        }

        protected BaseMessage(SerializationInfo info, StreamingContext context)
        {
            if (info == null) return;
            To = info.GetValue<IPeerIdentification>(nameof(To));
            From = info.GetValue<IPeerIdentification>(nameof(From));
            Type = info.GetValue<IMessageTypeIdentification>(nameof(Type));
            Version = info.GetValue<IMessageVersionIdentification>(nameof(Version));
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(To), To);
            info.AddValue(nameof(From), From);
            info.AddValue(nameof(Type), Type);
            info.AddValue(nameof(Version), Version);
        }
    }
}
