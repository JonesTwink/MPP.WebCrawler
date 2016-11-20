using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System;
using Logger;
namespace CrawlerLibrary
{
    internal class HtmlParser
    {
        private AngleSharp.Parser.Html.HtmlParser angleParser = new AngleSharp.Parser.Html.HtmlParser();
        private ILogger logger;

        public HtmlParser(ILogger logger)
        {
            this.logger = logger;
        }

        internal Task<List<string>> GetUrlsAsync(string url)
        {
            var parsingTask = Task.Factory.StartNew(() =>
            {
                try
                {
                    return GetUrls(url);
                }
                catch (Exception e)
                {
                    logger.WriteToLog(e);
                    return null;
                }

            });
            return parsingTask.Result;
        }

        private async Task<List<string>> GetUrls(string url)
        {
            var urls = new List<string>();
            string page = await GetPageContent(url);
            var document = await angleParser.ParseAsync(page);
            foreach (var element in document.QuerySelectorAll("a"))
            {
                
                string currentUrl = element.GetAttribute("href");
                
                if (currentUrl != null && currentUrl.Length>0)
                {
                    currentUrl = RelativeToAbsolute(currentUrl, url);
                    if (!urls.Contains(currentUrl))
                    {
                        urls.Add(currentUrl);
                    }
                }
            }
            return urls;
        }


        private async Task<string> GetPageContent(string url)
        {
            string pageContent = string.Empty;
            using (HttpClient webClient = new HttpClient())
            {
                try
                {
                    pageContent = await webClient.GetStringAsync(url);
                }
                catch(Exception e)
                {
                    logger.WriteToLog(e, url);    
                }           

            }
            return pageContent;
        }

        private string RelativeToAbsolute(string url, string parentUrl)
        {
            switch (url[0])
            {
                case '/':
                    if ((url.Length > 1) && (url[1] == '/'))
                        url = "http:" + url;
                    else
                        url = parentUrl + url;
                    break;
                case '#':
                    if (parentUrl[parentUrl.Length - 1] != '/')
                        url = parentUrl + '/' + url;
                    else
                        url = parentUrl + url;
                    break;
                case '?':
                    if (parentUrl[parentUrl.Length - 1] != '/')
                        url = parentUrl + '/' + url;
                    else
                        url = parentUrl + url;
                    break;
            }
            return url;
        }
    }    
}
