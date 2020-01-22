using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

namespace SecurityLab1_Starter.Models
{
    public class LogUtil
    {
        public void LogToFile(string logMessage, TextWriter w)
        {
            w.Write("\r\nLog Entry : ");
            w.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
            w.WriteLine($"  :{logMessage}");
            w.WriteLine("-------------------------------");
        }


        public void LogToEventViewer(string Source, string Message, EventLogEntryType type)
        {
            using (EventLog eventLog = new EventLog(Source))
            {
                eventLog.Source = Source;
                eventLog.WriteEntry(Message, type, 101, 1);
            }
        }
    }
}