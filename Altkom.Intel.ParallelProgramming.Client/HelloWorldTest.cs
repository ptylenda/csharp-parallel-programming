using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Altkom.Intel.ParallelProgramming.Client
{
    class HelloWorldTest
    {
        public static void Hello(string message)
        {
            Console.WriteLine("Sending...");
            Thread.Sleep(TimeSpan.FromSeconds(3));
            Console.WriteLine($"Sent: {message}");
        }
    }
}
