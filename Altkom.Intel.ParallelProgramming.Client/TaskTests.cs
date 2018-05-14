using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Altkom.Intel.ParallelProgramming.Client
{
    class TaskTests
    {
        public static void CreateTask()
        {
            for (int i = 0; i < 20; i++)
            {
                var test = Task.Factory.StartNew(() => { return Download("http://www.wp.pl"); });
                Console.WriteLine($"Calculated: {test.Result}");
            }
        }
        
        public static int Download(string uri)
        {
            Console.WriteLine($"Download: #{Thread.CurrentThread.ManagedThreadId}");
            using (var client = new WebClient())
            {
                var data = client.DownloadString(uri);
                Console.WriteLine($"#{Thread.CurrentThread.ManagedThreadId} {data.Trim().Substring(0, 50)}");

                return data.Length;
            }
        }
    }
}
