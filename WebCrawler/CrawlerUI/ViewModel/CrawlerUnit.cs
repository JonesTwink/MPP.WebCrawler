using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrawlerUI.Config;
using CrawlerLibrary;
using System.Windows.Controls;
namespace CrawlerUI.ViewModel
{
    class CrawlerUnit : ViewModelBase
    {
        private PreferencesLoader preferences = new PreferencesLoader();
        private ISimpleWebCrawler webCrawler  = new WebCrawler();
        private string _crawlResult;
        public string CrawlResult
        {
            get
            {
                return _crawlResult;
            }
            set
            {

                _crawlResult = value;
                RaisePropertyChanged("CrawlResult");
            }
        }

        internal void RunApplication()
        {
            preferences.LoadPreferences();
            webCrawler.MaxCrawlDepth = preferences.Depth;
            
            Task.Factory.StartNew(()=> {
                CrawlResult = webCrawler.PerformCrawlingAsync(preferences.URLs).Result.ToString();
                
            });

        }

        private TreeViewItem AddNesting(List<CrawlResult> ulrList)
        {
            TreeViewItem nodes = new TreeViewItem();
            foreach (CrawlResult item in ulrList)
            {
                
                TreeViewItem node = new TreeViewItem();
                node.Name = item.Url;
                if (item.Children != null)
                    node.Items.Add( AddNesting(item.Children) );
                nodes.Items.Add(node);
            }

            return nodes;
        }

    }
}
