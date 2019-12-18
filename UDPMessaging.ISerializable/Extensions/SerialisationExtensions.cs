using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace UDPMessaging.Extensions
{
    public static class SerialisationExtensions
    {
        public static T GetValue<T>(this SerializationInfo info, string name)
        {
            return (T)info.GetValue(name, typeof(T));
        }
    }
}