﻿using System;

namespace UDPMessaging.Exceptions
{
    public class ReceiveFailureException : UDPNetworkingException
    {
        public ReceiveFailureException(string errorMessage)
            : base(errorMessage) { }

        public ReceiveFailureException(string errorMessage, Exception innerException)
            : base(errorMessage, innerException) { }
    }
}