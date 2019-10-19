using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UDPMessaging.Utilities.ProcessingThread;

namespace UDPNetworkingTest.Utilities
{
    [TestClass]
    public class ProcessingThread
    {
        [TestMethod]
        public void TestMethod1()
        {
            ManualResetEvent manualResetEvent = new ManualResetEvent(true);


            //ProcessingHandleThread(WaitHandle waitConditionHandle, Action action, Func<Exception, bool> onException)
            ProcessingHandleThread pht = new ProcessingHandleThread(
                manualResetEvent, () => Console.WriteLine("Hello"),  (exception) => true
                );

            pht.Start();

            Thread.Sleep(100);

            //Known bug where can't close
            //The processing thread is faulty, understand cancellation tokens and get rid of it

            manualResetEvent.Set();
            manualResetEvent.Reset();
        }
    }
}
