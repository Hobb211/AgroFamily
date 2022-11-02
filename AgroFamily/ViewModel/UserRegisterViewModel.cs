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
using System.Collections;
using System.Diagnostics.Metrics;
using AgroFamily.View;

namespace AgroFamily.ViewModel
{

    public class UserRegisterViewModel : ViewModelBase
    {
        //Fields
        private string _id;
        private string _name;
        private string _lastname;
        private string _password;
        private string _typestring;
        private TypeUserModel _type;
        private ObservableCollection<TypeUserModel> _typeUser;
        public ObservableCollection<UserModel2> _users;
        public ObservableCollection<AdminModel> _adminsa;

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
        public ObservableCollection<AdminModel> Adminsa { get => _adminsa; set => _adminsa = value; }
        public ObservableCollection<UserModel2> Users { get => _users; set => _users = value; }

        //Commands
        public ICommand AddUserCommand { get; }
        public ICommand ShowUsersCommand { get; }

        //Constructor
        public UserRegisterViewModel()
        {
            AddUserCommand = new ViewModelCommand(ExecuteAddUserCommand, CanExecuteAddUserCommand);

            ITypeUserRepository typeUserRepository = new TypeUserRepository();
            TypeUser = typeUserRepository.GetByAll();

            //ShowUsersCommand = new ViewModelCommand(ObtenerDatosAdmins);
            //ShowUsersCommand = new ViewModelCommand(Action < AdminModel > ObtenerDatosAdmins);



            IUser2Repository usersRepository = new User2Repository();
            Users = usersRepository.GetByAll3();



            //IAdminRepository adminRepository = new AdminRepository();
            //Adminsa = adminRepository.GetByAll3();







            //IEnumerable<AdminModel> Adminsa2 = adminRepository.GetByAll();
            //Adminsa = new ObservableCollection<AdminModel>(Adminsa2);


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
            try
            {
                switch (Type.Name)
                {
                    case "Administrador":
                        //AdminModel admin = new AdminModel();
                        UserModel2 admin = new UserModel2();
                        admin.Id = Id;
                        admin.Name = Name;
                        admin.Lastname = Lastname;
                        admin.Password = Password;
                        admin.Type = "Administrador";
                        //IAdminRepository adminRepository = new AdminRepository();
                        IUser2Repository user2Repository = new User2Repository();
                        user2Repository.Add(admin);
                        break;

                    case "Cajero":
                        //CashierModel cashier = new CashierModel();
                        UserModel2 cashier = new UserModel2();
                        cashier.Id = Id;
                        cashier.Name = Name;
                        cashier.Lastname = Lastname;
                        cashier.Password = Password;
                        cashier.Type = "Cajero";
                        //ICashierRepository cashierRepository = new CashierRepository();
                        IUser2Repository user2Repository2 = new User2Repository();
                        user2Repository2.Add(cashier);
                        break;
                }


            }catch (Exception e)
            {
                MessageBox.Show("No se ha podido registrar"+e.Message);
                //throw new Exception("No se ha podido registrar");
            }
        }

        private void ExecuteGetData()
        {
            try
            {
                IAdminRepository repo = new AdminRepository();
                var myobscoll = new ObservableCollection<AdminModel>(repo.GetByAll2());

            }catch(Exception e)
            {
                MessageBox.Show("no se k paso");
            }
        }

    } 


}
