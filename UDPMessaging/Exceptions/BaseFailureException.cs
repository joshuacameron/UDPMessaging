using System;

namespace UDPMessaging.Exceptions
{
    public abstract class BaseFailureException : Exception
    {
        protected BaseFailureException(string errorMessage)
            : base(errorMessage) { }

        protected BaseFailureException(string errorMessage, Exception innerException)
            : base(errorMessage, innerException) { }
    }
}
