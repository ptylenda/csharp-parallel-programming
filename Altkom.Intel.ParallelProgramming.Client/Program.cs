using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altkom.Intel.ParallelProgramming.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            HelloWorldTest.Hello("Test");
            HelloWorldTest.Hello("Test");
            HelloWorldTest.Hello("Test");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
