using AgroFamily.Model;
using AgroFamily.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AgroFamily.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {

        //Fields
        private string _username;
        private SecureString _password;
        private string _errorMessage;
        private bool _isViewVisible=true;

        private IUserRepository userRepository;

        //Propierties
        public string Username { get => _username; set { _username = value; OnPropertyChanged(nameof(Username)); } }
        public SecureString Password { get => _password; set {  _password = value; OnPropertyChanged(nameof(Password)); } }
        public string ErrorMessage { get => _errorMessage; set { _errorMessage = value; OnPropertyChanged(nameof(ErrorMessage)); }
}
        public bool IsViewVisible { get => _isViewVisible; set { _isViewVisible = value; OnPropertyChanged(nameof(IsViewVisible)); }}
    
        //->Commands
        public ICommand LoginCommand { get; }
        public ICommand ShowPasswordCommand { get; }

        //Constructor
        public LoginViewModel()
        {
            userRepository = new UserRepository();
            LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
        }

        private bool CanExecuteLoginCommand(object obj)
        {
            bool validData;
            if (string.IsNullOrWhiteSpace(Username) || Username.Length < 3 || Password == null || Password.Length < 3)
            {
                validData = false;
            }
            else
            {
                validData = true;
            }
            return validData;
        }

        private void ExecuteLoginCommand(object obj)
        {
            NetworkCredential credential = new NetworkCredential(Username, Password);
            var isValidUser = userRepository.AuthenticateUser(credential);
            if (isValidUser)
            {
                var isValidPassword = userRepository.AuthenticatePassword(credential);
                if (isValidPassword)
                {
                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(Username), null/*userRepository.GetRole(new NetworkCredential(Username,Password))*/);
                    IsViewVisible = false;
                }
                else
                {
                    ErrorMessage = "Contraseña incorrecta";
                }
            }
            else
            {
                ErrorMessage = "El usuario no existe";
            }
        }
    }
}
