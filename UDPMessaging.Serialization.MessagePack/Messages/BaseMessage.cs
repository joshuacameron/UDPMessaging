using System;
using System.Collections.Generic;
using System.Text;
using MessagePack;
using UDPMessaging.Identification.MessageTypeIdentification;
using UDPMessaging.Identification.MessageVersionIdentification;
using UDPMessaging.Identification.PeerIdentification;
using UDPMessaging.Messages;

namespace UDPMessaging.Serialization.MessagePack.Messages
{
    [Union(0, typeof(StringMessage))]
    [MessagePackObject]
    public abstract class BaseMessage : IBaseMessage
    {
        [Key(0)]
        public IPeerIdentification To { get; set; }
        [Key(1)]
        public IPeerIdentification From { get; set; }
        [Key(2)]
        public IMessageTypeIdentification Type { get; }
        [Key(3)]
        public IMessageVersionIdentification Version { get; set; }

        protected BaseMessage(IMessageTypeIdentification type)
        {
            Type = type;
        }
    }
}
