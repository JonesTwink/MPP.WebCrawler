using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrawlerLibrary
{
    public interface ISimpleWebCrawler
    {
        int MaxCrawlDepth { get; set; }
        string Errors { get; set; }
        Task<CrawlResult> PerformCrawlingAsync(List<string> rootUrls);
    }
}
