using System;
using System.IO;

namespace BmsonStemOptimizer
{
    static class Logger
    {
        static DateTime start = DateTime.Now;
        static object _lock = new object();

        public delegate void LogEvent(string ev);

        public static event LogEvent OnLogEvent;

        static StreamWriter output = new StreamWriter("bso_log.txt", true);
        public static void Log(string s, params object[] list)
        {
            string ln = string.Format(s, list);
            string ts = string.Format("[{0}] {1}", DateTime.Now.ToString(), ln);

            lock (_lock)
            {
                OnLogEvent?.Invoke(ts);
                output.WriteLine(ts);
                output.Flush();
            }
        }
    }
}
