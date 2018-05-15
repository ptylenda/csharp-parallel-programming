using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altkom.Intel.ParallelProgramming.Client
{
    public static class Helpers
    {
        public static void MeasureTime(Action code)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            code();

            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed);
        }


        public static async Task MeasureTime(Func<Task> code)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            await code();

            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed);
        }
    }
}
