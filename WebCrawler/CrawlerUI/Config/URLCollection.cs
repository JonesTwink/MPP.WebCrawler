using System;
using System.Configuration;

using System.Collections.Generic;

namespace CrawlerUI.Config
{
    [ConfigurationCollection(typeof(URLElement), AddItemName = "url")]
    class URLCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new URLElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((URLElement)(element)).url;
        }

        public URLElement this[int idx]
        {
            get { return (URLElement)BaseGet(idx); }
        }
    }
}
