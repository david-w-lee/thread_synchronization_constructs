using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace thread_synchronization_constructs
{
    public class CountdownEventTest
    {
        /// <summary>
        /// CountdownEvent doesn't allow thread to continue at sync.Wait() until the count goes down to 0.
        /// This is normally used for fork-and-join scenarios.
        ///                Thread 1
        /// MainThread --> Thread 2  --> MainTread
        ///                Thread 3
        ///                
        /// </summary>
        private void Test1()
        {
            CountdownEvent sync = new CountdownEvent(3);
            Task.Run(() =>
            {
                Console.WriteLine($"Thread1");
                sync.Signal();
            });

            Task.Run(() =>
            {
                Console.WriteLine($"Thread2");
                sync.Signal();
            });

            Task.Run(() =>
            {
                Console.WriteLine($"Thread3");
                sync.Signal();
            });
            sync.Wait();
        }

        public void Run()
        {
            Test1();
        }
    }
}
