using System;
using System.Collections.Generic;
using System.Text;
using MessagePack;
using UDPMessaging.Serialization.MessagePack.Identification.MessageTypeIdentification;

namespace UDPMessaging.Serialization.MessagePack.Messages
{
    public class StringMessage : BaseMessage
    {
        [Key(4)]
        public string Data { get; set; }

        public StringMessage()
            : base(new TypeMessageTypeIdentification(typeof(StringMessage)))
        {
        }
    }
}
