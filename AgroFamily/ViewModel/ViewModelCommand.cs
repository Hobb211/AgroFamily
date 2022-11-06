using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AgroFamily.ViewModel
{
    //implementamos la interzaf comando 
    internal class ViewModelCommand : ICommand
    {
        //Fields
        //Definimos un campo de tipo action para encapsular un metodo, podemos pasar un metodo como parametro
        private readonly Action<object> _executeAction;
        //Creamos un predicate para verificar si la accion se puede realizar o no 
        private readonly Predicate<object> _canExecuteAction;

        //Constructors
        //Se crea el constructor con los campos de arriba 
        //Se crean dos constructores porque uno es por si no se realiza la accion 

        //Si no se realiza la accion 
        public ViewModelCommand(Action<object> executeAction)
        {
            _executeAction = executeAction;
            _canExecuteAction = null;
        }

        //Si se realiza la accion 
        public ViewModelCommand(Action<object> executeAction, Predicate<object> canExecuteAction)
        {
            _executeAction = executeAction;
            _canExecuteAction = canExecuteAction;
        }

        //Events
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        //Methods
        public bool CanExecute(object parameter)
        {
            return _canExecuteAction == null? true: _canExecuteAction(parameter);
        }

        public void Execute(object parameter)
        {
            _executeAction(parameter);
        }
    }
}
