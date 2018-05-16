using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Altkom.Intel.ParallelProgramming.Client
{
    class ParallelAggregationTests
    {
        public static void CalcuateTest()
        {
            Calculate(Enumerable.Range(0, 10000).Select(x => (double)x).ToArray());
        }

        public static double Calculate(double[] sequence)
        {
            var lockObject = new object();
            double sum = 0.0d;

            Parallel.ForEach(
                // The values to be aggregated 
                sequence,

                // The local initial partial result
                () => 0.0d,

                // The loop body
                (x, loopState, partialResult) =>
                {
                    return Normalize(x) + partialResult;
                },          
                (localPartialSum) =>
                {
                    // Enforce serial access to single, shared result
                    lock (lockObject)
                    {
                        sum += localPartialSum;
                    }
                });

            return sum;
        }

        private static double Normalize(double x)
        {
            Console.WriteLine($"Normalize: #{Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(TimeSpan.FromSeconds(2));
            return x / 2.0;
        }
    }
}
