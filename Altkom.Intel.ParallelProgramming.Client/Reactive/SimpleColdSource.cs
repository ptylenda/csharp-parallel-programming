using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altkom.Intel.ParallelProgramming.Client.Reactive
{
    class SimpleColdSource : IObservable<string>
    {
        public IDisposable Subscribe(IObserver<string> observer)
        {
            observer.OnNext("Test");
            observer.OnNext("123");
            observer.OnCompleted();
            return EmptyDisposable.Instance;
        }

        private class EmptyDisposable : IDisposable
        {
            public static EmptyDisposable Instance = new EmptyDisposable();

            public void Dispose()
            {
                Console.WriteLine("Disposed");
            }
        }
    }
}
