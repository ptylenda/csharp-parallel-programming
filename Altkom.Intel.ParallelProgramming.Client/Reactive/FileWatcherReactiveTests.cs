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

            using (createdObservable.Subscribe(x => Console.WriteLine($"Created {x.FullPath}")))
            using (deletedObservable.Subscribe(x => Console.WriteLine($"Deleted {x.FullPath}")))
            {
                Console.WriteLine("Press any key to stop watching file system...");
                Console.ReadKey();
            }
        }
    }
}
