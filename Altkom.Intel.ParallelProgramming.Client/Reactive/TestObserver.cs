using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altkom.Intel.ParallelProgramming.Client.Reactive
{
    class TestObserver : IObserver<string>
    {
        public TestObserver(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }

        public void OnCompleted()
        {
            Console.WriteLine($"[{this.Name}] End!");
        }

        public void OnError(Exception error)
        {
            Console.WriteLine($"[{this.Name}] Error: {error}");
        }

        public void OnNext(string value)
        {
            Console.WriteLine($"[{this.Name}] Received: {value}");
        }
    }
}
