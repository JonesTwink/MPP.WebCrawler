using System.Threading.Tasks;
using CrawlerLibrary;
using CrawlerUI.Config;

namespace CrawlerUI.Model
{
    internal class CrawlerModel
    {
        private PreferencesLoader preferences = new PreferencesLoader();
        private ISimpleWebCrawler webCrawler = new WebCrawler();

        internal async Task<CrawlResult> RunApplication()
        {
            preferences.LoadPreferences();
            webCrawler.MaxCrawlDepth = preferences.Depth;

            return await webCrawler.PerformCrawlingAsync(preferences.URLs);           

        }
    }
}
