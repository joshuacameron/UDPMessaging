using System.Runtime.Serialization;

namespace UDPMessaging.Identification
{
    public interface IIdentification : ISerializable
    {
        object GetIdentification();
    }
}
