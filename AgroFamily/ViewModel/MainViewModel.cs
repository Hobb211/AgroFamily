using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
        private Visibility _navigationMenuVisibility;
        private Visibility _inventoryMenuVisibility;
        private Visibility _businessMenuVisibility;
        private Visibility _adminMenuVisibility;
        private bool _isViewVisible = true;
        

        public bool IsViewVisible { get => _isViewVisible; set { _isViewVisible = value; OnPropertyChanged(nameof(IsViewVisible)); } }


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
        public Visibility NavigationMenuVisibility { get => _navigationMenuVisibility; set { _navigationMenuVisibility = value; OnPropertyChanged(nameof(NavigationMenuVisibility)); } }
        public Visibility InventoryMenuVisibility { get => _inventoryMenuVisibility; set { _inventoryMenuVisibility = value; OnPropertyChanged(nameof(InventoryMenuVisibility)); } }
        public Visibility BusinessMenuVisibility { get => _businessMenuVisibility; set { _businessMenuVisibility = value; OnPropertyChanged(nameof(BusinessMenuVisibility)); } }
        public Visibility AdminMenuVisibility { get => _adminMenuVisibility; set { _adminMenuVisibility = value; OnPropertyChanged(nameof(AdminMenuVisibility)); } }

        //Commands navigation
        public ICommand ShowNavigationMenuCommand { get; }
        public ICommand ShowInventoryMenuCommand { get; }
        public ICommand ShowBusinessMenuCommand { get; }

        //Commands
        public ICommand ShowAddItemsViewCommand { get;}
        public ICommand ShowAddExpensiveViewCommand { get;}
        public ICommand ShowAddUserViewCommand { get; }
        public ICommand ShowInventoryViewCommand { get; }
        public ICommand ShowCashRegisterViewCommand { get; }
        public ICommand ShowEditStockViewCommand { get; }
        public ICommand ShowBusinessStatusViewCommand { get; }
        public ICommand ShowSaleHistoryCommand { get; }
        public ICommand ShowProductProfitabilityViewCommand { get; }
        public ICommand LogOutCommand { get; }
        public ICommand MaxMinFontCommand { get; }

        public MainViewModel()
        {
            userRepository = new UserRepository();
            UserAccount = new UserAccountModel();
            LoadCurrentUserData();

            //Initialize navigation commands
            ShowNavigationMenuCommand = new ViewModelCommand(ExecuteShowNavigationMenuCommand);
            ShowInventoryMenuCommand= new ViewModelCommand(ExecuteShowInventoryMenuCommand);
            ShowBusinessMenuCommand= new ViewModelCommand(ExecuteShowBusinessMenuCommand);

            //Initialize commands
            ShowAddItemsViewCommand = new ViewModelCommand(ExecuteShowAddItemsViewCommand);
            ShowAddExpensiveViewCommand = new ViewModelCommand(ExecuteShowAddExpensiveViewCommand);
            ShowAddUserViewCommand = new ViewModelCommand(ExecuteShowAddUserViewCommand);
            ShowInventoryViewCommand = new ViewModelCommand(ExecuteShowInventoryViewCommand);
            ShowCashRegisterViewCommand = new ViewModelCommand(ExecuteShowCashRegisterViewCommand);
            ShowEditStockViewCommand = new ViewModelCommand(ExecuteShowEditStockViewCommand);
            ShowBusinessStatusViewCommand = new ViewModelCommand(ExecuteShowBusinessStatusViewCommand);
            ShowProductProfitabilityViewCommand = new ViewModelCommand(ExecuteShowBusinessStatusViewCommand);
            ShowSaleHistoryCommand = new ViewModelCommand(ExecuteShowSaleHistoryCommand);
            LogOutCommand = new ViewModelCommand(ExecuteLogOutCommand);
            MaxMinFontCommand = new ViewModelCommand(ExecuteMaxMinFontCommand);

            //Default view
            ExecuteShowCashRegisterViewCommand(null);
            InventoryMenuVisibility = Visibility.Collapsed;
            BusinessMenuVisibility = Visibility.Collapsed;
            if (UserAccount.Role != "Administrador")
            {
                AdminMenuVisibility = Visibility.Collapsed;
            }
            else
            {
                AdminMenuVisibility= Visibility.Visible;
            }
            Application.Current.Properties["IsViewMinimize"] = true;
            TextSize = 12;
            TitleSize = 20;
        }

        private void ExecuteMaxMinFontCommand(object obj)
        {
            ChangeSizeFont();
            if ((bool)Application.Current.Properties["IsViewMinimize"])
            {
                Application.Current.Properties["IsViewMinimize"] = false;
            }
            else
            {
                Application.Current.Properties["IsViewMinimize"] = true;
            }
        }

        private void ExecuteLogOutCommand(object obj)
        {
            IsViewVisible= false;
        }

        private void ExecuteShowSaleHistoryCommand(object obj)
        {
            CurrentChildView = new SaleHistoryViewModel();
        }

        private void ExecuteShowEditStockViewCommand(object obj)
        {
            CurrentChildView=new EditStockViewModel();
        }

        private void ExecuteShowNavigationMenuCommand(object obj)
        {
            InventoryMenuVisibility = Visibility.Collapsed;
            BusinessMenuVisibility = Visibility.Collapsed;
            NavigationMenuVisibility = Visibility.Visible;
        }

        private void ExecuteShowBusinessMenuCommand(object obj)
        {
            InventoryMenuVisibility = Visibility.Collapsed;
            BusinessMenuVisibility = Visibility.Visible;
            NavigationMenuVisibility = Visibility.Collapsed;
        }

        private void ExecuteShowInventoryMenuCommand(object obj)
        {
            InventoryMenuVisibility = Visibility.Visible;
            BusinessMenuVisibility = Visibility.Collapsed;
            NavigationMenuVisibility = Visibility.Collapsed;
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
        private void ExecuteShowInventoryViewCommand(object obj)
        {
            CurrentChildView = new InventoryViewModel();
        }

        private void ExecuteShowAddExpensiveViewCommand(object obj)
        {
            CurrentChildView = new AddExpensiveViewModel();
        }


        private void ExecuteShowBusinessStatusViewCommand(object obj)
        {
            CurrentChildView = new BusinessStatusViewModel();
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
