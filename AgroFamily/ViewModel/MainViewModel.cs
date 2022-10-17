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
        public ICommand ShowAddUserViewCommand { get; }

        public MainViewModel()
        {
            userRepository = new UserRepository();
            UserAccount = new UserAccountModel();
            LoadCurrentUserData();

            //Initialize commands
            ShowAddItemsViewCommand = new ViewModelCommand(ExecuteShowAddItemsViewCommand);
            ShowAddUserViewCommand = new ViewModelCommand(ExecuteShowAddUserViewCommand);

            //Default view
            //ExecuteShowAddItemsViewCommand(null);
            ExecuteShowAddUserViewCommand(null);
        }

        private void ExecuteShowAddItemsViewCommand(object obj)
        {
            CurrentChildView = new AddItemsViewModel();
        }
        private void ExecuteShowAddUserViewCommand(object obj)
        {
            CurrentChildView = new UserRegisterViewModel();
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
