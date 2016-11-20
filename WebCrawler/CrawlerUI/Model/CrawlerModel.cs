using System.Threading.Tasks;
using CrawlerLibrary;
using CrawlerUI.Config;
using Logger;

namespace CrawlerUI.Model
{
    internal class CrawlerModel
    {
        private PreferencesLoader preferences = new PreferencesLoader();
        private ISimpleWebCrawler webCrawler;
        private ILogger logger;

        public CrawlerModel(ILogger logger)
        {
            this.logger = logger;
            webCrawler = new WebCrawler(logger);
        }
                
        internal async Task<CrawlResult> RunApplication()
        {
            preferences.LoadPreferences();
            webCrawler.MaxCrawlDepth = preferences.Depth;
            return await webCrawler.PerformCrawlingAsync(preferences.URLs);      
        }
    }
}
