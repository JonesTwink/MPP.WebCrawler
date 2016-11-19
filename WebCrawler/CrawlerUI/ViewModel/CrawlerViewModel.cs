using System.Threading.Tasks;
using CrawlerLibrary;
using CrawlerUI.Model;

namespace CrawlerUI.ViewModel
{
    internal class CrawlerViewModel : ViewModelBase
    {

        private CrawlerModel model = new CrawlerModel();
        private CrawlResult _crawlResult;
        public CrawlResult CrawlResult
        {
            get
            {
                return _crawlResult;
            }
            set
            {
                _crawlResult = value;
                RaisePropertyChanged(nameof(CrawlResult));
            }
        }

        private AsyncCrawlingCommand _asyncCrawlingCommand;
        public AsyncCrawlingCommand AsyncCrawlingCommand
        {
            get
            {
                return _asyncCrawlingCommand;
            }
        }
        internal CrawlerViewModel()
        {
            _asyncCrawlingCommand  = new AsyncCrawlingCommand(async () => {
                if (_asyncCrawlingCommand.CanExecute)
                {
                    _asyncCrawlingCommand.CanExecute = false;
                    CrawlResult = await model.RunApplication();
                    _asyncCrawlingCommand.CanExecute = true;
                }
            });
        }

    }
}
