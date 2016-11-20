using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logger;

namespace CrawlerLibrary
{
    public class WebCrawler : ISimpleWebCrawler
    {
        private HtmlParser htmlParser;
        private const int InitialCrawlDepthLevel = 0;
        public int MaxCrawlDepth { get; set; } = 1;
        private ILogger logger;

        public WebCrawler(ILogger logger)
        {
            this.logger = logger;
            htmlParser = new HtmlParser(logger);
        }

        public async Task<CrawlResult> PerformCrawlingAsync(List<string> rootUrls)
        {
            return await PerformCrawlingAsync(rootUrls, "Root", InitialCrawlDepthLevel);
        }

        public async Task<CrawlResult> PerformCrawlingAsync(List<string> rootUrls, string parentUrl, int currentCrawlDepth)
        {
            currentCrawlDepth++;

            List<CrawlResult> results = new List<CrawlResult>();
            foreach (string currentUrl in rootUrls)
            {
                results.Add(await AddToResultsAsync(currentUrl, currentCrawlDepth));
            }

            return new CrawlResult(parentUrl, results);
        }

        private async Task<CrawlResult> AddToResultsAsync(string currentUrl, int currentCrawlDepth)
        {
            List<string> childUrls = await htmlParser.GetUrlsAsync(currentUrl);

            CrawlResult currentPageCrawlResult;

            if (currentCrawlDepth < MaxCrawlDepth)
            {
                currentPageCrawlResult = await PerformCrawlingAsync(childUrls, currentUrl, currentCrawlDepth);
            }
            else
            {
                currentPageCrawlResult = new CrawlResult(currentUrl, StringToCrawlResult(childUrls));
            }
            return currentPageCrawlResult;
        }

        private List<CrawlResult> StringToCrawlResult(List<string> stringUrls)
        {
            List<CrawlResult> CrawlResultUrls = new List<CrawlResult>();
            foreach (string url in stringUrls)
            {
                CrawlResultUrls.Add(new CrawlResult(url, null));
            }
            return CrawlResultUrls;
        }
    }
}