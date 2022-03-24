using System;

namespace TextEditor.Commands
{
    internal class RelayCommand : BaseCommand
    {
        public RelayCommand(Action<object> executeMethod, bool canExecuteCommand = true)
        {
            _executeMethod = executeMethod ?? throw new ArgumentNullException("executeMethod can not be null");
            CanExecuteCommand = canExecuteCommand;
        }

        public override void ExecuteCommand(object parameter) => _executeMethod(parameter);

        private readonly Action<object> _executeMethod;
    }
}
