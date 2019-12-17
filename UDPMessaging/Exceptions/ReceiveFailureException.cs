using System;

namespace UDPMessaging.Exceptions
{
    public class ReceiveFailureException : BaseFailureException
    {
        public ReceiveFailureException(string errorMessage, Exception innerException)
            : base(errorMessage, innerException) { }
    }
}
