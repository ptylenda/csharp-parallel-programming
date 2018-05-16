using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altkom.Intel.ParallelProgramming.Client.Reactive
{
    class FileWatcherReactiveTests
    {
        public static void Execute()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var watcher = new FileSystemWatcher(path)
            {
                IncludeSubdirectories = true,
                EnableRaisingEvents = true
            };

            var createdObservable = Observable
                .FromEventPattern<FileSystemEventHandler, FileSystemEventArgs>(
                    h => watcher.Created += h,
                    h => watcher.Created -= h)
                .Select(x => x.EventArgs);

            var deletedObservable = Observable
                .FromEventPattern<FileSystemEventHandler, FileSystemEventArgs>(
                    h => watcher.Deleted += h,
                    h => watcher.Deleted -= h)
                .Select(x => x.EventArgs);

            var createdRateObservable = createdObservable
                .Window(TimeSpan.FromSeconds(3))
                .Select(x => x.Count());

            using (createdObservable.Subscribe(new ConsoleWritingObserver<FileSystemEventArgs>("Created", x => x.FullPath)))
            using (deletedObservable.Subscribe(new ConsoleWritingObserver<FileSystemEventArgs>("Deleted", x => x.FullPath)))
            using (createdObservable.Where(x => Path.GetExtension(x.FullPath) == ".txt").Subscribe(new ConsoleWritingObserver<FileSystemEventArgs>("Created txt", x => x.FullPath)))
            using (deletedObservable.Where(x => Path.GetExtension(x.FullPath) == ".txt").Subscribe(new ConsoleWritingObserver<FileSystemEventArgs>("Deleted txt", x => x.FullPath)))
            using (createdRateObservable.Subscribe(x => x.Subscribe(y => Console.WriteLine($"[Created rate/s]: {(y / 3.0)}"))))
            {
                Console.WriteLine("Press any key to stop watching file system...");
                Console.ReadKey();
            }
        }
    }
}
