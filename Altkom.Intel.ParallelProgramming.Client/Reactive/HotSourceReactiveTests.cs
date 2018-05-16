using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace Altkom.Intel.ParallelProgramming.Client.Reactive
{
    class HotSourceReactiveTests
    {
        public static void SubjectTest()
        {
            var source = new Subject<string>();
            source.OnNext("Test");
            source.OnNext("Hot");
            source.OnNext("Source");

            var observer = new TestObserver("Test1");
            Console.WriteLine("Subscribing Test1");
            source.Subscribe(observer);

            source.OnNext("New Test");

            Console.WriteLine("Subscribing Test1 again");
            source.Subscribe(observer);

            source.OnNext("New Hot");
            source.OnNext("New Source");

            var observer2 = new TestObserver("Test2");
            Console.WriteLine("Subscribing Test2");
            var subscription = source.Subscribe(observer2);

            source.OnNext("A");
            source.OnNext("B");

            source.OnError(new Exception("Critical error"));

            Console.WriteLine("Unsubscribing Test1");
            subscription.Dispose();
            source.OnNext("C");

            source.OnCompleted();
        }
    }
}
