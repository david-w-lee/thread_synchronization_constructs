using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace thread_synchronization_constructs
{
    public class ManualResetEventTest
    {

        /// <summary>
        /// ManualResetEvent is used for blocking other threads before a thread completes an activity.
        /// ManualResetEvent is designed for controlling flow of threads.
        /// Some people use it in the opposite way, i.e. make main thread WaitOne() until childthreads Set(), which seems like CountdownEvent is the right fit.
        /// </summary>
        private void Test1()
        {
            bool initialState = false;
            ManualResetEvent sync = new ManualResetEvent(initialState);

            Task.Run(() =>
            {
                // If you pass in milliseconds, it works like Timeout time. i.e., it waits for the state to change only until given time.
                sync.WaitOne();
                Console.WriteLine("Thread1");
            });

            Task.Run(() =>
            {
                sync.WaitOne();
                Console.WriteLine("Thread2");
            });

            Console.WriteLine("MainThread sleep for 3 sec");
            Thread.Sleep(3000);
            // This will change state to true and will remain true. So, Thread2 and Thread3 will execute.
            sync.Set();
            // If you want to block Thread 3, call sync.Reset()
            //sync.Reset();

            Task.Run(() =>
            {
                sync.WaitOne();
                Console.WriteLine("Thread3");
            });
        }

        /// <summary>
        /// ManualResetEventSlim behaves in the same way as ManualResetEvent except that this hybrid construct 
        /// defers kernel-mode construct until the first contention.
        /// </summary>
        private void Test2()
        {
            bool initialState = false;
            ManualResetEventSlim sync = new ManualResetEventSlim(initialState);

            Task.Run(() =>
            {
                sync.Wait();
                Console.WriteLine("Thread1");
            });

            Task.Run(() =>
            {
                sync.Wait();
                Console.WriteLine("Thread2");
            });

            Console.WriteLine("MainThread sleep for 3 sec");
            Thread.Sleep(3000);
            // This will change state to true and will remain true. So, Thread2 and Thread3 will execute.
            sync.Set();

            Task.Run(() =>
            {
                sync.Wait();
                Console.WriteLine("Thread3");
            });
        }

        public void Run()
        {
            Test1();
        }
    }
}
