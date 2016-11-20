using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public interface ILogger
    {
        void WriteToLog(string message);
        void WriteToLog(Exception e);
        void WriteToLog(Exception e, string errorContextInfo);
        string GetLog();
    }
}
