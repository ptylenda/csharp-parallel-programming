using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Altkom.Intel.ParallelProgramming.Client
{
    class TaskTests
    {
        public static void TaskRunTest()
        {
            Console.WriteLine($"DoWork: #{Thread.CurrentThread.ManagedThreadId} UI");
            DoWorkAsync();
            DoWorkAsync2();
        }

        public static Task<decimal> DoWorkAsync()
        {
            return Task.Run(() => DoWork());
        }

        public static Task<decimal> DoWorkAsync2()
        {
            return Task.FromResult(DoWork());
        }

        public static decimal DoWork()
        {
            Console.WriteLine($"DoWork: #{Thread.CurrentThread.ManagedThreadId} working...");
            Thread.Sleep(TimeSpan.FromSeconds(3));
            Console.WriteLine($"DoWork: #{Thread.CurrentThread.ManagedThreadId} finished");
            return 10;
        }

        public static void CreateTask()
        {
            for (int i = 0; i < 50; i++)
            {
                var test = Task.Factory.StartNew(() => { return Download("http://www.wp.pl"); });
                Console.WriteLine($"Calculated: {test.Result}");
            }
        }

        public static void ExecuteSyncTasks()
        {
            var timer = new Stopwatch();
            timer.Start();

            var tasks = new List<Task>();
            for (int i = 0; i < 500; i++)
            {
                tasks.Add(Task.Factory.StartNew(() => { return TaskTests.Download("http://www.wp.pl"); }));
            }
            Task.WaitAll(tasks.ToArray());

            timer.Stop();
            Console.WriteLine($"Download requests took {timer.Elapsed}");
        }

        public static void ExecuteAsyncTasks()
        {
            var timer = new Stopwatch();
            timer.Start();

            var tasks = new List<Task>();
            for (int i = 0; i < 500; i++)
            {
                tasks.Add(TaskTests.AsyncTestShort());
            }
            Task.WaitAll(tasks.ToArray());

            timer.Stop();
            Console.WriteLine($"Download requests took {timer.Elapsed}");
        }

        public static async Task AsyncTest()
        {
            await DownloadAsync("http://wp.pl");
            Console.WriteLine("After wp");
            await DownloadAsync("http://onet.pl");
            Console.WriteLine("After onet");
            await DownloadAsync("http://intel.pl");
            Console.WriteLine("After intel");
        }

        public static async Task AsyncTestShort()
        {
            await DownloadAsync("http://wp.pl");
            Console.WriteLine("After wp");
        }

        public static async Task AsyncTestWithCancel()
        {
            var r = new Random();
            var source = new CancellationTokenSource(TimeSpan.FromMilliseconds(200));
            var token = source.Token;

            try
            {
                await DownloadAsync("http://wp.pl", token);
                Console.WriteLine("After wp");
            }
            catch (WebException e) when (e.Status == WebExceptionStatus.RequestCanceled)
            {
                Console.WriteLine("Request cancelled!");
            }            
        }

        public static int Download(string uri)
        {
            Console.WriteLine($"Download: #{Thread.CurrentThread.ManagedThreadId}");
            using (var client = new WebClient())
            {
                Thread.Sleep(5000);
                var data = client.DownloadString(uri);
                Console.WriteLine($"#{Thread.CurrentThread.ManagedThreadId} {data.Trim().Substring(0, 50)}");

                return data.Length;
            }
        }

        public static async Task<int> DownloadAsync(string uri)
        {
            Console.WriteLine($"Download: #{Thread.CurrentThread.ManagedThreadId}");
            using (var client = new WebClient())
            {
                await Task.Delay(5000);
                var data = await client.DownloadStringTaskAsync(uri);
                Console.WriteLine($"#{Thread.CurrentThread.ManagedThreadId} {data.Trim().Substring(0, 50)}");

                return data.Length;
            }
        }

        public static async Task<int> DownloadAsync(string uri, CancellationToken token)
        {
            Console.WriteLine($"Download: #{Thread.CurrentThread.ManagedThreadId}");
            using (var client = new WebClient())
            using (token.Register(() => client.CancelAsync()))
            {
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine("Cancelled before");
                    return 0;
                }
                else
                {
                    var data = await client.DownloadStringTaskAsync(uri);
                    Console.WriteLine($"#{Thread.CurrentThread.ManagedThreadId} {data.Trim().Substring(0, 50)}");
                    return data.Length;
                }
            }
        }
    }
}
