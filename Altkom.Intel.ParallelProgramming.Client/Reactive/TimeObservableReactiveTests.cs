using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Altkom.Intel.ParallelProgramming.Client.Reactive
{
    class TimeObservableReactiveTests
    {
        public static void TimerTest()
        {
            var r = new Random();
            var source = Observable
                            .Interval(TimeSpan.FromMilliseconds(250))
                            .Select(_ => r.Next(100))
                            .Finally(() => Console.WriteLine("Source finished producing messages."))
                            .Publish();  // Broadcasting to all subscribers
            source.Connect();

            var alerts = source.Where(s => s > 60);

            var subscriber = new TestObserver("Test1");
            Console.WriteLine("Subscribing Test1");
            using (source.Select(x => x.ToString()).Subscribe(subscriber))
            {
                Thread.Sleep(TimeSpan.FromSeconds(10));
            }


            Console.WriteLine("Subscribing console alert (temp > 60) using lambda");
            using (alerts.Subscribe(x => Console.WriteLine($"ALERT! Temperature is above 60: {x}")))
            {
                Console.WriteLine("Subscribing console writer using lambda");
                using (source.Subscribe(x => Console.WriteLine(x)))
                {
                    Thread.Sleep(TimeSpan.FromSeconds(10));
                }
            }
        }
    }
}
