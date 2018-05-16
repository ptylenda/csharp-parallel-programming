using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altkom.Intel.ParallelProgramming.Client.Reactive
{
    class ConsoleWritingObserver<T> : IObserver<T>
    {
        private readonly Func<T, string> toStringFunc;

        public ConsoleWritingObserver(string name)
            : this(name, x => x.ToString())
        {
        }


        public ConsoleWritingObserver(string name, Func<T, string> toStringFunc)
        {
            this.Name = name;
            this.toStringFunc = toStringFunc;
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

        public void OnNext(T value)
        {
            Console.WriteLine($"[{this.Name}] Received: {this.toStringFunc(value)}");
        }
    }
}
