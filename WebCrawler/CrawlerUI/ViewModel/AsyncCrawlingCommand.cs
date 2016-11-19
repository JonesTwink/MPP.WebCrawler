using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CrawlerUI.ViewModel
{
    class AsyncCrawlingCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly Func<Task> command;
        private bool canExecute = true;

        public AsyncCrawlingCommand(Func<Task> command)
        {
            this.command = command;
        }

        public async void Execute(object parameter)
        {
            await ExecuteAsync(parameter);
        }

        bool ICommand.CanExecute(object parameter)
        {
            return canExecute;
        }
        public Task ExecuteAsync(object parameter)
        {
            return command();
        }

        public bool CanExecute
        {
            get
            {
                return canExecute;
            }
            set
            {
                if (canExecute != value)
                {
                    canExecute = value;
                    EventHandler canExecuteChanged = CanExecuteChanged;
                    if (canExecuteChanged != null)
                    {
                        canExecuteChanged(this, EventArgs.Empty);
                    }
                }
            }
        }
    }
}
