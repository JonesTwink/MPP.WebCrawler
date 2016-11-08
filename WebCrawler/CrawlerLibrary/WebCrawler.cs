using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrawlerLibrary
{
    public class WebCrawler: ISimpleWebCrawler
    {
        private HtmlParser htmlParser = new HtmlParser();
        private const int InitialCrawlDepthLevel = 0;

        public int MaxCrawlDepth { get; set; } = 1;

        public WebCrawler()
        {
        }

        public async Task<CrawlResult> PerformCrawlingAsync(List<string> rootUrls)
        {
            return await PerformCrawlingAsync(rootUrls, "Root", InitialCrawlDepthLevel);
        }

        public async Task<CrawlResult> PerformCrawlingAsync(List<string> rootUrls, string parentUrl, int currentCrawlDepth)
        {
            if (currentCrawlDepth >= MaxCrawlDepth)
                return new CrawlResult();
            currentCrawlDepth++;

            var results = new List<CrawlResult>();

            foreach (string currentUrl in rootUrls )
            {
                List<string> childUrls = await htmlParser.GetUrlsAsync(currentUrl);
                CrawlResult currentPageCrawlResult = await PerformCrawlingAsync(childUrls, currentUrl, currentCrawlDepth);
                                
                results.Add(currentPageCrawlResult);
            }

            return new CrawlResult(parentUrl, results);
        }
    }
}
