using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace thread_synchronization_constructs
{
    public class AutoResetEventTest
    {
        /// <summary>
        /// AutoResetEvent allows only one thread to execute at a time.
        /// Once sync.WaitOne() is called, state automatically changes to false.
        /// This is designed for giving exclusive access to a shared source.
        /// </summary>
        private void Test1()
        {
            bool initialState = false;
            AutoResetEvent sync = new AutoResetEvent(initialState);

            int count = 0;
            Task.Run(() =>
            {
                sync.WaitOne();
                count++;
                Console.WriteLine($"Thread1: {count}");
                //sync.Set();
            });

            Task.Run(() =>
            {
                sync.WaitOne();
                count++;
                Console.WriteLine($"Thread2: {count}");
                //sync.Set();
            });

            Console.WriteLine("MainThread sleep for 3 sec");
            Thread.Sleep(3000);
            // This will change state to true but will set to false after one of the threads is unblocked.
            // So, Thread2 and Thread3 will not execute unless sync.Set() is set two more times.
            sync.Set();

            Task.Run(() =>
            {
                sync.WaitOne();
                count++;
                Console.WriteLine($"Thread3: {count}");
            });
        }


        public void Run()
        {
            Test1();
        }
    }
}
