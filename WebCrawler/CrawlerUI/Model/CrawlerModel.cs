using System.Threading.Tasks;
using CrawlerLibrary;
using CrawlerUI.Config;

namespace CrawlerUI.Model
{
    internal class CrawlerModel
    {
        private PreferencesLoader preferences = new PreferencesLoader();
        private ISimpleWebCrawler webCrawler = new WebCrawler();
        public string Errors { get; set; } = string.Empty;
        internal async Task<CrawlResult> RunApplication()
        {
            preferences.LoadPreferences();
            webCrawler.MaxCrawlDepth = preferences.Depth;

            var result = await webCrawler.PerformCrawlingAsync(preferences.URLs);
            if (webCrawler.Errors.Length > 0)
            {
                Errors += webCrawler.Errors;
                webCrawler.Errors = string.Empty;
            }
            return result;         

        }
    }
}
