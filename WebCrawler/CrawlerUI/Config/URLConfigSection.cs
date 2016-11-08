using System;
using System.Configuration;

namespace CrawlerUI.Config
{
    class URLConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("URLs")]
        public URLCollection URLs
        {
            get { return ((URLCollection)(base["URLs"])); }
        }
    }
}
