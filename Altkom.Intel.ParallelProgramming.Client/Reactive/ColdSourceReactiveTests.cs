using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altkom.Intel.ParallelProgramming.Client.Reactive
{
    class ColdSourceReactiveTests
    {
        public static void Execute()
        {
            var source = new SimpleColdSource();
            var observer = new TestObserver("Test");

            using (source.Subscribe(observer))
            {

            }
        }
    }
}
