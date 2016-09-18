using System;
using System.IO;
using System.Threading;

namespace DeenGames.InfiniteArpg
{
    public class FileWatcher
    {
        public bool Stop { get; set; }
        private DateTime lastUpdated;

        // TODO: thread pooling or something.

        public void Watch(string file, Action onUpdateCallback)
        {
            this.lastUpdated = File.GetLastWriteTime(file);
            this.Stop = false;

            new Thread(new ThreadStart(() =>
            {
                while (this.Stop == false)
                {
                    var currentWriteTime = File.GetLastWriteTime(file);

                    if (lastUpdated != currentWriteTime)
                    {
                        lastUpdated = currentWriteTime;
                        if (onUpdateCallback != null)
                        {
                            onUpdateCallback.Invoke();
                        }

                        #if DEBUG
                        Console.WriteLine(string.Format("{0} updated at {1}", file, lastUpdated));
                        #endif
                    }

                    Thread.Sleep(100);
                }
            })).Start();
        }
    }
}

