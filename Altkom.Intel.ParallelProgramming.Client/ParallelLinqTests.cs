using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Altkom.Intel.ParallelProgramming.Client
{
    class ParallelLinqTests
    {
        public static void SequentialTest()
        {
            var charArray = "abcdefg1234567890".Select(x => char.ToUpper(x)).ToArray();
            string up = new string(charArray);
            Console.WriteLine(up);
        }

        public static void ParallelTest()
        {
            var charArray = "abcdefg1234567890".AsParallel().Select(x => char.ToUpper(x)).ToArray();
            string up = new string(charArray);
            Console.WriteLine(up);
        }

        public static void ParallelTestWithThreadNumbers()
        {
            for (int i = 0; i < 30; i++)
            {
                var chars = "abcdefg1234567890"
                    .AsParallel()
                    .Select(x =>
                        {
                            Console.Write($"#{Thread.CurrentThread.ManagedThreadId}:{x} ");
                            return char.ToUpper(x);
                        })
                    .ToArray();

                Console.Write($"  {new string(chars)}");
                Console.WriteLine();
            }

            Console.WriteLine();
        }

        public static void ParallelTestWithThreadNumbersAsOrdered()
        {
            for (int i = 0; i < 30; i++)
            {
                var chars = "abcdefg1234567890"
                    .AsParallel()
                    .AsOrdered()
                    .Select(x =>
                    {
                        Console.Write($"#{Thread.CurrentThread.ManagedThreadId}:{x} ");
                        return char.ToUpper(x);
                    })
                    .ToArray();

                Console.Write($"  {new string(chars)}");
                Console.WriteLine();
            }

            Console.WriteLine();
        }
    }
}
