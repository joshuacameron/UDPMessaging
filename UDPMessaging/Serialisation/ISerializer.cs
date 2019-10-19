using System;
using System.Collections.Generic;
using System.Text;
using UDPMessaging.Messages;

namespace UDPMessaging.Serialisation
{
    public interface ISerializer
    {
        byte[] Serialize(IBaseMessage message);
        IBaseMessage Deserialize(byte[] messageBytes);
    }
}
