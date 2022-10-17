using AgroFamily.Model;
using AgroFamily.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace AgroFamily.ViewModel
{

    public class UserRegisterViewModel : ViewModelBase
    {
        //Fields
        private string _id;
        private string _name;
        private string _lastname;
        private string _password;
        private TypeUserModel _type;
        private ObservableCollection<TypeUserModel> _typeUser;

        //Propierties
        public string Id { get => _id; set { _id = value; OnPropertyChanged(nameof(Id)); } }
        public string Name { get => _name; set { _name = value; OnPropertyChanged(nameof(Name)); } }
        public string Lastname { get => _lastname; set { _lastname = value; OnPropertyChanged(nameof(Lastname)); } }
        public string Password { get => _password; set { _password = value; OnPropertyChanged(nameof(Password)); } }
        public TypeUserModel Type
        {
            get => _type;
            set
            {
                _type = value;
                OnPropertyChanged(nameof(Type));

            }
        }
        public ObservableCollection<TypeUserModel> TypeUser { get => _typeUser; set => _typeUser = value; }

        //Commands
        public ICommand AddUserCommand { get; }

        //Constructor
        public UserRegisterViewModel()
        {
            AddUserCommand = new ViewModelCommand(ExecuteAddUserCommand, CanExecuteAddUserCommand);
            ITypeUserRepository typeUserRepository = new TypeUserRepository();
            TypeUser = typeUserRepository.GetByAll();
        }

        private bool CanExecuteAddUserCommand(object obj)
        {
            bool validData;
            if (string.IsNullOrWhiteSpace(Name) || Name.Length < 3 || Type == null)
            {
                validData = false;
            }
            else
            {
                if (Type.Name.Equals("Administrador"))
                {
                    validData = true;
                }
                else { validData = true; }
            }
            return validData;
        }

        private void ExecuteAddUserCommand(object obj)
        {
            switch (Type.Name)
            {
                case "Administrador":
                    AdminModel admin = new AdminModel();
                    admin.Id = Id;
                    admin.Name = Name;
                    admin.Lastname = Lastname;
                    admin.Password = Password;
                    IAdminRepository productRepository = new AdminRepository();
                    productRepository.Add(admin);
                    break;

                case "Cajero":
                    CashierModel cashier = new CashierModel();
                    cashier.Id = Id;
                    cashier.Name = Name;
                    cashier.Lastname = Lastname;
                    cashier.Password = Password;
                    ICashierRepository cashierRepository = new CashierRepository();
                    cashierRepository.Add(cashier);
                    break;
            }
        }




    } 

}
