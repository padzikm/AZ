using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace GolfersUI
{
    public class RelayCommandWithParams<T> : ICommand
    {
        protected readonly Func<Boolean> canExecute;

        protected readonly Action<T> execute;

        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (this.canExecute != null)
                {
                    CommandManager.RequerySuggested += value;
                }
            }

            remove
            {
                if (this.canExecute != null)
                {
                    CommandManager.RequerySuggested -= value;
                }
            }
        }

        public RelayCommandWithParams(Action<T> execute, Func<Boolean> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public RelayCommandWithParams(Action<T> execute)
            : this(execute, null)
        {
        }

        public virtual Boolean CanExecute(Object parameter)
        {
            return this.canExecute == null ? true : this.canExecute();
        }

        public virtual void Execute(Object parameter)
        {
            this.execute((T)parameter);
        }
    }
}
