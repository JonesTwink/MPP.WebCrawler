using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public class Logger : ILogger
    {
        public static Logger Instance { get { return lazy.Value; } }
        private static readonly Lazy<Logger> lazy = new Lazy<Logger>(() => new Logger());
        private Logger()
        {
        }

        private StringBuilder stringBuilder = new StringBuilder();

        public void WriteToLog(string message)
        {
            stringBuilder.AppendFormat("{0}\r\n", message);
        }

        public void WriteToLog(Exception e)
        {
            if (e.InnerException != null)
                stringBuilder.AppendFormat("{0}\r\n", e.InnerException.Message);
            else
                stringBuilder.AppendFormat("{0}\r\n", e.Message);
        }
        public void WriteToLog(Exception e, string errorContextInfo)
        {
            if (e.InnerException != null)
                stringBuilder.AppendFormat("{0} : {1}\r\n", errorContextInfo, e.InnerException.Message);
            else
                stringBuilder.AppendFormat("{0} : {1}\r\n", errorContextInfo, e.Message);
        }

        public string GetLog()
        {
            return stringBuilder.ToString();
        }

    }
}
