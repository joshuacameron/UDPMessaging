namespace UDPMessaging.Identification.MessageTypeIdentification
{
    public interface IMessageTypeIdentification : IIdentification
    {
        bool Equals(IMessageTypeIdentification obj);
    }
}
