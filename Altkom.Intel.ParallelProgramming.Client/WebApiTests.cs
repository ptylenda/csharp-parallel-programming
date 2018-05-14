using Altkom.Intel.ParallelProgramming.Service.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Altkom.Intel.ParallelProgramming.Client
{
    class WebApiTests
    {
        private const string BaseUrl = "http://localhost:56757/api/robot/";

        public static async Task GetRobots()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("");
                var robots = await response.Content.ReadAsAsync<List<Robot>>();

                foreach (var robot in robots)
                {
                    Console.WriteLine(robot);
                }
            }
        }

        public static void ExecuteAsyncRobotsCall()
        {
            var timer = new Stopwatch();
            timer.Start();

            var tasks = new List<Task>();
            for (int i = 0; i < 500; i++)
            {
                tasks.Add(GetRobots());
            }
            Task.WaitAll(tasks.ToArray());

            timer.Stop();
            Console.WriteLine($"Download requests took {timer.Elapsed}");
        }
    }
}
