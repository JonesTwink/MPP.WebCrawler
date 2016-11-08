using System;

namespace CrawlerUI.Config
{
    class PreferencesException : Exception
    {
        public PreferencesException()
        {
        }

        public PreferencesException(string message)
        : base(message)
        {
        }

        public PreferencesException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }
}
