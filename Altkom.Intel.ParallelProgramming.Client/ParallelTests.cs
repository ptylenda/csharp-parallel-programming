using Altkom.Intel.ParallelProgramming.Service.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Altkom.Intel.ParallelProgramming.Client
{
    class ParallelTests
    {
        private const string BaseUrl = "http://localhost:56757/api/robot/";

        public static async Task ForSequentialTest()
        {
            await Helpers.MeasureTime(async () =>
            {
                for (int i = 0; i < 1000; i++)
                {
                    await SequentialTest();
                }
            });
        }

        public static void ForParallelTest()
        {
            Helpers.MeasureTime(() =>
            {
                Parallel.For(0, 1000, i =>
                {
                    SequentialTest().GetAwaiter().GetResult();
                });
            });
        }

        public static async Task SequentialTest()
        {
            Console.WriteLine($"SequentialTest: #{Thread.CurrentThread.ManagedThreadId}");

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("");
                //await Task.Delay(5000);
                var robots = await response.Content.ReadAsAsync<List<Robot>>();

                foreach (var robot in robots)
                {
                    Console.WriteLine(robot);
                }
            }
        }
    }
}
