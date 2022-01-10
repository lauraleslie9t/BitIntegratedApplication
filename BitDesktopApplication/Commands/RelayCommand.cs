using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BitDesktopApplication.Commands
{
  
    public class RelayCommand : ICommand //Listener on Windows and when an input is found the connection command gets executed with the help of this class
    {
        private Action _whatToExecute;
        //events are public 
        public event EventHandler CanExecuteChanged;
        private bool _canExecute;
        public RelayCommand(Action what, bool canExecute) //takes boolian because every event has dormant events. Need to activate the event essentially.
        {
            _whatToExecute = what;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }
        public void Execute(object parameter)
        {
            _whatToExecute.Invoke();
        }
    }
}


