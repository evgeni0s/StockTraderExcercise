using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace Infrastructure
{
    public class RelayCommand : ICommand
    {
        private Action<object> onTestCommandExecute;

        public RelayCommand(Func<object, bool> validateExecution, Action<object, object> executeCommand)
        {
            ValidateExecution = validateExecution;
            ExecuteCommand = executeCommand;
        }

        public RelayCommand(Action<object, object> executeCommand)
        {
            // ValidateExecution = validateExecution;
            ExecuteCommand = executeCommand;
        }

        public RelayCommand(Action<object> onTestCommandExecute)
        {
            this.onTestCommandExecute = onTestCommandExecute;
        }

        public Func<object, bool> ValidateExecution { get; set; }
        public Action<object, object> ExecuteCommand { get; set; }

        public event EventHandler CanExecuteChanged;

        bool ICommand.CanExecute(object parameter)
        {
            if (this.ValidateExecution != null)
            {
                return this.ValidateExecution(parameter);
            }
            else return true;
        }

        //event EventHandler ICommand.CanExecuteChanged
        //{
        //    add { CommandManager.RequerySuggested += value; }
        //    remove { CommandManager.RequerySuggested -= value; }
        //}

        void ICommand.Execute(object sender)
        {
            if (ExecuteCommand != null)
            {
                this.ExecuteCommand(sender, null);
            }
            else
            {
                this.onTestCommandExecute(sender);
            }
        }
    }
}
