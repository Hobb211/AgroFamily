using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using AgroFamily.Model;
using AgroFamily.Repositories;
using AgroFamily.View;
using FontAwesome.Sharp;

namespace AgroFamily.ViewModel
{
    public class MainViewModel:ViewModelBase
    {
        //Fields
        private UserAccountModel _userAccount;
        private IUserRepository userRepository;
        private ViewModelBase _currentChildView;

        //Properties
        public UserAccountModel UserAccount 
        { 
            get => _userAccount;
            set
            {
                _userAccount = value;
                OnPropertyChanged(nameof(UserAccount));
            }
        }

        public ViewModelBase CurrentChildView 
        { 
            get => _currentChildView; 
            set 
            { 
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            } 
        }

        //Commands
        public ICommand ShowAddItemsViewCommand { get;}

        public ICommand ShowAddExpensiveViewCommand { get;}

        public ICommand ShowAddUserViewCommand { get; }

        public ICommand ShowCashRegisterViewCommand { get; }

        public MainViewModel()
        {
            userRepository = new UserRepository();
            UserAccount = new UserAccountModel();
            LoadCurrentUserData();

            //Initialize commands
            ShowAddItemsViewCommand = new ViewModelCommand(ExecuteShowAddItemsViewCommand);
            ShowAddExpensiveViewCommand = new ViewModelCommand(ExecuteShowAddExpensiveViewCommand);
            ShowAddUserViewCommand = new ViewModelCommand(ExecuteShowAddUserViewCommand);
            ShowCashRegisterViewCommand = new ViewModelCommand(ExecuteShowCashRegisterViewCommand);
            
            //Default view
            ExecuteShowCashRegisterViewCommand(null);
        }

        private void ExecuteShowCashRegisterViewCommand(object obj)
        {
            CurrentChildView = new CashRegisterViewModel();
        }

        private void ExecuteShowAddItemsViewCommand(object obj)
        {
            CurrentChildView = new AddItemsViewModel();
        }
        private void ExecuteShowAddUserViewCommand(object obj)
        {
            CurrentChildView = new UserRegisterViewModel();
        }

        private void ExecuteShowAddExpensiveViewCommand(object obj)
        {
            CurrentChildView = new AddExpensiveViewModel();
        }

        private void LoadCurrentUserData()
        {
            var user = userRepository.GetById(Thread.CurrentPrincipal.Identity.Name);//Pasar un UserName por el principal
            if (user != null)
            {
                UserAccount.UserName = user.Id;
                UserAccount.Name = user.Name;
                UserAccount.LastName = user.LastName;
                UserAccount.Role = user.Type;
            }
            else
            {
                MessageBox.Show("Usuario invalido");
                Application.Current.Shutdown();
            }
        }

        
    }
}
