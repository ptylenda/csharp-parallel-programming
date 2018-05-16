using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altkom.Intel.ParallelProgramming.Client.Reactive
{
    class MergeSourceReactiveTests
    {
        public static void ConcatTests()
        {
            var room1 = Observable.Range(1, 5);
            var room2 = Observable.Range(10, 10);

            var mergedRoom = room1.Concat(room2);

            mergedRoom.Subscribe(x => Console.WriteLine(x));
        }
    }
}
