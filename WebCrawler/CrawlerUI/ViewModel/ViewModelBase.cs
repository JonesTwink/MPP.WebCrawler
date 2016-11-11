using System;
using System.ComponentModel;


namespace CrawlerUI.ViewModel
{
    class ViewModelBase : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

    }
}
