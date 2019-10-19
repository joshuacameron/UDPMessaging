using System;
using System.Collections.Generic;
using System.Text;

namespace UDPMessaging.Utilities.ProcessingThread
{
    internal interface IProcessingThread : IDisposable
    {
        void Start();
        void Stop();
    }
}
