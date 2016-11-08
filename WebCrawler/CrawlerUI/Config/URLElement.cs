using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrawlerUI.Config
{
    class URLElement : ConfigurationElement
    {
        [ConfigurationProperty("url", DefaultValue = "", IsKey = true, IsRequired = true)]
        public string url
        {
            get { return ((string)(base["url"])); }
            set { base["url"] = value; }
        }
    }
}
