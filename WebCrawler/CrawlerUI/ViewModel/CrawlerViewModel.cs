﻿using System.Threading.Tasks;
using CrawlerLibrary;
using CrawlerUI.Model;
using System.Windows;
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
        internal CrawlerViewModel()
        {
            _asyncCrawlingCommand  = new AsyncCrawlingCommand(async () => {
                if (_asyncCrawlingCommand.CanExecute)
                {
                    IsCrawling = Visibility.Visible;
                    _asyncCrawlingCommand.CanExecute = false;
                    CrawlResult = await model.RunApplication();
                    _asyncCrawlingCommand.CanExecute = true;
                    IsCrawling = Visibility.Hidden;
                }
            });
        }

    }
}
