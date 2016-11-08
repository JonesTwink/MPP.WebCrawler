using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrawlerLibrary
{
    internal class HtmlParser
    {
        internal Task<List<string>> GetUrlsAsync(string url)
        {
            var parsingTask = Task.Factory.StartNew(() =>
            {
                return GetUrls(url);
            });
            return parsingTask;
        }

        private List<string> GetUrls(string url)
        {
            return new List<string> { url+"1", url+"2", url+"3" };
        }

    }    
}
