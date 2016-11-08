using System.Collections.Generic;

namespace CrawlerLibrary
{
    public class CrawlResult
    {
        public string Url { get; private set; }
        public   List<CrawlResult> Children { get; private set; }

        public CrawlResult()
        {
        }

        public CrawlResult(string url, List<CrawlResult> children)
        {
            Url = url;
            Children = children;
        }
    }
}
