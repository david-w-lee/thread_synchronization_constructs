using System;

namespace thread_synchronization_constructs
{
    class Program
    {
        static void Main(string[] args)
        {
            var o = new ManualResetEventTest();
            o.Run();

            Console.WriteLine("done");
            Console.ReadLine();
        }
    }
}
