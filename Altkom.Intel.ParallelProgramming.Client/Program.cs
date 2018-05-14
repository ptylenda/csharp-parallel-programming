﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Altkom.Intel.ParallelProgramming.Client
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine($"Main: #{Thread.CurrentThread.ManagedThreadId}");

            /*HelloWorldTest.Hello("Test");
            HelloWorldTest.Hello("Test");
            HelloWorldTest.Hello("Test");*/

            /*ThreadsTest.CreateThread();
            ThreadsTest.CreateThread();
            ThreadsTest.CreateThread();
            ThreadsTest.CreateThread();*/

            //ThreadsTest.CreateDowloadThread();

            //ThreadPoolTest.CreateThreadPoolTest();

            //TaskTests.CreateTask();

            TaskTests.ExecuteSyncTasks();
            TaskTests.ExecuteAsyncTasks();

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
