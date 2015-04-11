using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfersUI
{
    public class RelayAsyncCommand : RelayCommand, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }

        private bool isExecuting = false;

        public bool IsExecuting
        {
            get
            {
                return isExecuting;
            }

            set
            {
                isExecuting = value;
                OnPropertyChanged(new PropertyChangedEventArgs("IsExecuting"));
            }
        }

        public event EventHandler Started;

        public event EventHandler Ended;



        public RelayAsyncCommand(Action execute, Func<Boolean> canExecute)
            : base(execute, canExecute)
        {
        }

        public RelayAsyncCommand(Action execute)
            : base(execute)
        {
        }

        public override Boolean CanExecute(Object parameter)
        {
            return ((base.CanExecute(parameter)) && (!this.IsExecuting));
        }

        public override void Execute(object parameter)
        {
            try
            {
                this.IsExecuting = true;
                if (this.Started != null)
                {
                    this.Started(this, EventArgs.Empty);
                }

                Task task = Task.Factory.StartNew(() =>
                {
                    this.execute();
                });
                task.ContinueWith(t =>
                {
                    this.OnRunWorkerCompleted(EventArgs.Empty);
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
            catch (Exception ex)
            {
                this.OnRunWorkerCompleted(new RunWorkerCompletedEventArgs(null, ex, true));
            }
        }

        private void OnRunWorkerCompleted(EventArgs e)
        {
            this.IsExecuting = false;
            if (this.Ended != null)
            {
                this.Ended(this, e);
            }
        }
    }
}
