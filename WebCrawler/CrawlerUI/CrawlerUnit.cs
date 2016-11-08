using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrawlerUI.Config;
using CrawlerLibrary;

namespace CrawlerUI
{
    class CrawlerUnit
    {
        private PreferencesLoader preferences = new PreferencesLoader();
        private ISimpleWebCrawler webCrawler  = new WebCrawler();


        internal void RunApplication()
        {
            preferences.LoadPreferences();
            webCrawler.MaxCrawlDepth = preferences.Depth;
            var result =  webCrawler.PerformCrawlingAsync(preferences.URLs); 
            
        }

    }
}
