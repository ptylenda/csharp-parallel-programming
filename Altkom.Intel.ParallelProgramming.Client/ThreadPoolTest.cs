using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Altkom.Intel.ParallelProgramming.Client
{
    class ThreadPoolTest
    {
        public static void CreateThreadPoolTest()
        {
            for (int i = 0; i < 50; i++)
            {
                ThreadPool.QueueUserWorkItem(_ => Download("http://www.wp.pl/"));
            }
        }

        public static void Download(string uri)
        {
            Console.WriteLine($"Download: #{Thread.CurrentThread.ManagedThreadId}");
            using (var client = new WebClient())
            {
                var data = client.DownloadString(uri);
                Console.WriteLine($"#{Thread.CurrentThread.ManagedThreadId} {data.Trim().Substring(0, 50)}");
            }
        }
    }
}
