using System.Runtime.Serialization;
using UDPMessaging.Identification.MessageTypeIdentification;
using UDPMessaging.Identification.MessageVersionIdentification;
using UDPMessaging.Identification.PeerIdentification;

namespace UDPMessaging.Messages
{
    public interface IBaseMessage
    {
        IPeerIdentification To { get; set; }
        IPeerIdentification From { get; set; }
        IMessageTypeIdentification Type { get; }
        IMessageVersionIdentification Version { get; set; }
    }
}
