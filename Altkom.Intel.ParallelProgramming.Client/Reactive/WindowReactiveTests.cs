using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altkom.Intel.ParallelProgramming.Client.Reactive
{
    class WindowReactiveTests
    {
        public static void Execute()
        {
            var r = new Random();
            int n = 100;

            var source = Observable
                .Interval(TimeSpan.FromMilliseconds(5))
                .Select(_ => r.Next(n))
                .Publish();
            var bufferedSource = source.Buffer(10);
            source.Connect();

            var histogram = new Dictionary<int, int>();
            void Increment(double value)
            {
                int key = (int)value / 1;
                int count = 0;
                histogram.TryGetValue(key, out count);
                histogram[key] = count+1;
            }

            using (source.Subscribe(new ConsoleWritingObserver<int>("Item")))
            using (bufferedSource.Subscribe(new ConsoleWritingObserver<IList<int>>("Average of last 10 items", x => (x.Average()).ToString())))
            using (bufferedSource.Subscribe(x => Increment(x.Average())))
            {
                Console.WriteLine("Press any key to stop generating items...");
                Console.ReadKey();
            }

            Console.WriteLine();
            Console.WriteLine("Your histogram yoooo!");

            int max = histogram.Values.Max();
            int height = 30;
            double normBlock = (double)height / max;

            for (int currentHeight = height-1; currentHeight >= 0; currentHeight--)
            {
                for (int i = 0; i < n/1; i++)
                {
                    if (histogram.TryGetValue(i, out int count) && count * normBlock > currentHeight)
                    {
                        Console.Write("#");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }

                Console.WriteLine();
            }
        }
    }
}
