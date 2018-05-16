using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altkom.Intel.ParallelProgramming.Client.Reactive
{
    class CpuReactiveTests
    {
        public static void Execute()
        {
            var cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");

            var source = Observable
                .Interval(TimeSpan.FromMilliseconds(50))
                .Select(_ => cpuCounter.NextValue())
                .Publish();
            source.Connect();

            var alertSource = source.Where(x => x > 15.0);

            using (source.Subscribe(new ConsoleWritingObserver<float>("CPU usage %")))
            using (alertSource.Subscribe(new ConsoleWritingObserver<float>("ALERT! CPU usage > 15%")))
            {
                Console.WriteLine("Press any key to stop watching CPU performance counters...");
                Console.ReadKey();
            }

            const float lambda = 0.85f;
            var emaSource = source.Scan(float.NaN, (current, next) =>
            {
                if (float.IsNaN(current))
                {
                    return next;
                }
                else
                {
                    return current * lambda + next * (1 - lambda);
                }
            });

            alertSource = emaSource.Where(x => x > 25.0);

            using (emaSource.Subscribe(new ConsoleWritingObserver<float>("CPU usage % (EMA)")))
            using (alertSource.Subscribe(new ConsoleWritingObserver<float>("ALERT! CPU usage > 25% (EMA)")))
            {
                Console.WriteLine("Press any key to stop watching CPU performance counters...");
                Console.ReadKey();
            }
        }
    }
}
