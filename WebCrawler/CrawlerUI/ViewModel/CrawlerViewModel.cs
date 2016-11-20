using System.Threading.Tasks;
using System;
using CrawlerLibrary;
using CrawlerUI.Model;
using System.Windows;
using Logger;
namespace CrawlerUI.ViewModel
{
    internal class CrawlerViewModel : ViewModelBase
    {
        private ILogger logger;

        private CrawlerModel model;

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

        private Visibility _isCrawling = Visibility.Hidden;
        public Visibility IsCrawling
        {
            get { return this._isCrawling; }
            private set
            {
                if (this._isCrawling != value)
                {
                    this._isCrawling = value;
                    this.RaisePropertyChanged(nameof(IsCrawling));
                }
            }
        }

        private string _messages;
        public string Messages
        {
            get
            {
                return _messages;
            }
            set
            {
                _messages = value;
                RaisePropertyChanged(nameof(Messages));
            }
        }
        internal CrawlerViewModel()
        {
            logger = Logger.Logger.Instance;
            model = new CrawlerModel(logger);

            _asyncCrawlingCommand  = new AsyncCrawlingCommand(async () => {
                if (_asyncCrawlingCommand.CanExecute)
                {
                    IsCrawling = Visibility.Visible;
                    _asyncCrawlingCommand.CanExecute = false;
                    try
                    {
                        CrawlResult = await model.RunApplication();
                    }
                    catch(Exception e)
                    {
                        logger.WriteToLog(e.Message);
                    }

                    Messages = logger.GetLog();
                    _asyncCrawlingCommand.CanExecute = true;
                    IsCrawling = Visibility.Hidden;
                }
            });
        }

    }
}
