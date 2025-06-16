using System.ComponentModel;
using System.Windows.Input;

namespace NET.Paint.Drawing.Mvvm
{
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public RelayCommand(Action execute, Func<bool> canExecute, INotifyPropertyChanged notifier, params string[] propertyNames)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;

            if (notifier != null)
            {
                notifier.PropertyChanged += (s, e) =>
                {
                    if (propertyNames.Contains(e.PropertyName))
                        RaiseCanExecuteChanged();
                };
            }
        }

        public bool CanExecute(object? parameter)
        {
            return _canExecute == null || _canExecute();
        }

        public void Execute(object? parameter)
        {
            _execute();
        }

        public event EventHandler? CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
