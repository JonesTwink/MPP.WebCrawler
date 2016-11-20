using System.Collections.Generic;
using Logger;
using System.Threading.Tasks;

namespace CrawlerLibrary
{
    public interface ISimpleWebCrawler
    {
        int MaxCrawlDepth { get; set; }
        Task<CrawlResult> PerformCrawlingAsync(List<string> rootUrls);
    }
}
