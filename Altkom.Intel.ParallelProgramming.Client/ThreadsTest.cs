using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Altkom.Intel.ParallelProgramming.Client
{
    class ThreadsTest
    {
        public static void Hello(string message)
        {
            Console.WriteLine($"Hello: #{Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine("Sending...");
            Thread.Sleep(TimeSpan.FromSeconds(3));
            Console.WriteLine($"Sent: {message}");
        }

        public static void CreateThread()
        {
            var thread = new Thread(DoWork);
            thread.IsBackground = true;
            thread.Start();
            //thread.Join();
        }

        public static void DoWork()
        {
            Hello("Hello 123");
        }


        public static void CreateDowloadThread()
        {
            /*var thread = new Thread(Download);
            thread.Start("http://www.wp.pl/");

            var thread2 = new Thread(Download);
            thread2.Start("http://www.intel.pl/");*/

            var thread = new Thread(() => Download("http://www.wp.pl/"));
            thread.Start();

            var thread2 = new Thread(() => Download("http://www.intel.pl/"));
            thread2.Start();
        }


        public static void Download(object uri)
        {
            Download((string)uri);
        }

        public static void Download(string uri)
        {
            Console.WriteLine($"Download: #{Thread.CurrentThread.ManagedThreadId}");
            using (var client = new WebClient())
            {
                var data = client.DownloadString(uri);
                Console.WriteLine(data.Trim().Substring(0, 256));
            }
        }
    }
}
