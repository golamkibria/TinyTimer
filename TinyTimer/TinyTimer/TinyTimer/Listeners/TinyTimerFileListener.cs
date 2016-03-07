using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KNet.TinyTimer.Listeners
{
    using System.Diagnostics;
    using System.IO;
    using System.Threading;
    //TODO: Flush Timer

    public class TinyTimerFileListener : ITinyTimerTraceListener
    {
        private string _logFolder;
        private string _logFilePath;

        private List<TinyTimerTraceInfo> _infoItems = new List<TinyTimerTraceInfo>();
        
        public TinyTimerFileListener(string logFolder)
        {
            this._logFolder = Directory.Exists(logFolder) ? Path.GetFullPath(logFolder) : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, logFolder);
            
            this._logFilePath = Path.Combine(this._logFolder, DateTime.Now.ToString("yyy-MM-dd hh-mm-ss") + ".log");
            Debug.WriteLine($"LogFile: {this._logFilePath}");

            this.BufferSize = 100;
        }

        public int BufferSize {get;set;}
        
        public void Take(params TinyTimerTraceInfo[] infoItems)
        {
            this._infoItems.AddRange(infoItems);

            if (this._infoItems.Count >= this.BufferSize)
                SaveInfoItems();
        }

        public void Flush()
        {
            SaveInfoItems();
        }


        private void SaveInfoItems()
        {
            if (this._infoItems.Count == 0)
                return;

            var infoItems = this._infoItems.ToArray();
            this._infoItems.Clear();

            Task.Run(() =>
            {
                CheckParentFolder(this._logFolder);

                try
                {
                    Debug.WriteLine("TinyTimerFileListener: Take");
                    using (var file = TryOpen(this._logFilePath))
                    {
                        if (file == null)
                            return;

                        foreach (var item in infoItems)
                        {
                            file.WriteLine(item.ToString());
                        }
                        file.Close();
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"TinyTimerFileListener:Task: Exception: {e.Message}");
                    throw;
                }
            });
        }

        private static void CheckParentFolder(string logFolder)
        {
            try
            {
                if (!Directory.Exists(logFolder))
                {
                    Directory.CreateDirectory(logFolder);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"TinyTimerFileListener:CheckParentFolder: Exception: {e.Message}");
                throw;
            }
        }

        private static StreamWriter TryOpen(string filePath, int maximumAttempts = 10, int attemptWaitMS = 1000)
        {
            StreamWriter fs = null;
            int attempts = 0;

            // Loop allow multiple attempts
            while (true)
            {
                try
                {
                    fs = new StreamWriter(File.Open(filePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read));                                        
                    break;
                }
                catch (IOException ioEx)
                {
                    Debug.WriteLine(ioEx.Message);
                    Debug.WriteLine(ioEx.StackTrace);

                    attempts++;
                    if (attempts > maximumAttempts)
                    {
                        fs = null;
                        break;
                    }
                    else
                    {
                        Thread.Sleep(attempts * attemptWaitMS);
                    }
                }
            }

            return fs;
        }       
    }
}
