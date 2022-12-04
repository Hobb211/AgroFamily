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
using System.IO;

namespace AgroFamily.ViewModel
{

    public class UserRegisterViewModel : ViewModelBase
    {
        //Fields
        private string _id;
        private string _name;
        private string _lastname;
        private string _password;
        private string _newPassword;
        private TypeUserModel _type;
        private ObservableCollection<TypeUserModel> _typeUser;
        private ObservableCollection<UserModel> _users;
        private UserModel _currentUser;

        //Propierties
        public string Id { get => _id; set { _id = value; OnPropertyChanged(nameof(Id)); } }
        public string Name { get => _name; set { _name = value; OnPropertyChanged(nameof(Name)); } }
        public string Lastname { get => _lastname; set { _lastname = value; OnPropertyChanged(nameof(Lastname)); } }
        public string Password { get => _password; set { _password = value; OnPropertyChanged(nameof(Password)); } }
        public string NewPassword { get => _newPassword; set { _newPassword = value; OnPropertyChanged(nameof(NewPassword)); } }
        public UserModel CurrentUser { get => _currentUser; set { _currentUser = value; OnPropertyChanged(nameof(CurrentUser)); } }
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
        public ObservableCollection<UserModel> Users { get => _users; set { _users = value; OnPropertyChanged(nameof(Users)); } }

        //Commands
        public ICommand AddUserCommand { get; }
        public ICommand RemoveUserCommand { get; }
        public ICommand UpdatePasswordCommand { get; }
        public ICommand ExportCSV { get; }

        //Constructor
        public UserRegisterViewModel()
        {
            AddUserCommand = new ViewModelCommand(ExecuteAddUserCommand, CanExecuteAddUserCommand);
            RemoveUserCommand = new ViewModelCommand(ExecuteRemoveUserCommand, CanExecuteRemoveUserCommand);
            UpdatePasswordCommand = new ViewModelCommand(ExecuteUpdateUserPasswordCommand, CanExecuteUpdateUserPasswordCommand);
            ExportCSV = new ViewModelCommand(ExecuteExportCSV);
            ITypeUserRepository typeUserRepository = new TypeUserRepository();
            TypeUser = typeUserRepository.GetByAll();
            IUserRepository usersRepository = new UserRepository();
            Users = usersRepository.GetByAll();

            TextSizeChange = 10;
            ButtonChangeSizeH = 20;
            ButtonChangeSizeW = 20;
            if ((bool)Application.Current.Properties["IsViewMinimize"])
            {
                TextSize = 6;
                TitleSize = 8;
                ButtonHeight1 = 10;
                ButtonWidth1 = 60;
                ButtonHeight2 = 20;
                ButtonWidth2 = 140;
            }
            else
            {
                TextSize = 36;
                TitleSize = 38;
                ButtonHeight1 = 70;
                ButtonWidth1 = 120;
                ButtonHeight2 = 80;
                ButtonWidth2 = 200;
            }
        }

        private void ExecuteExportCSV(object obj)
        {
            MessageBox.Show("Iniciando conversión");
            using (var textWriter = File.CreateText(@"C:\Users\jimimix\Desktop\mio\apuntes universidad\2022_SEMESTRE_2        x\Ingeniería de software\CSV_test\testing.csv"))
            {
                //string columns_line = "ID,Nombre,Apellido,Rol,Clave";
                //textWriter.WriteLine(columns_line);
                foreach (var line in ToCsv(Users))
                {
                    textWriter.WriteLine(line);
                }
            }
        }

        public static IEnumerable<string> ToCsv<UserModel>(IEnumerable<UserModel> list)
        {
            var fields = typeof(UserModel).GetFields();
            var properties = typeof(UserModel).GetProperties();

            foreach (var @object in list)
            {
                yield return string.Join(",",
                                         fields.Select(x => (x.GetValue(@object) ?? string.Empty).ToString())
                                               .Concat(properties.Select(p => (p.GetValue(@object, null) ?? string.Empty).ToString()))
                                               .ToArray());
            }
        }

        private void ExecuteRemoveUserCommand(object obj)
        {
            try
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("¿Estás seguro de eliminar el usuario?", "Confirmación de eliminación", System.Windows.MessageBoxButton.YesNo);

                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    IUserRepository userRepository = new UserRepository();
                    userRepository.Remove(CurrentUser.Id);
                    Users = userRepository.GetByAll();
                    MessageBox.Show("Usuario eliminado");
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("No se ha podido eliminar, " + e.Message);

            }


        }
        private bool CanExecuteRemoveUserCommand(object obj) => CurrentUser == null ? false : true;
        private bool CanExecuteUpdateUserPasswordCommand(object obj) {
            bool validData;
            if (NewPassword == null || CurrentUser == null)
            {
                validData = false;
            }
            else
            {
                validData = true;
            }
            return validData;
        }
        private void ExecuteUpdateUserPasswordCommand(object obj){
            try
            {
                IUserRepository userRepository = new UserRepository();
                CurrentUser.Password = NewPassword;
                userRepository.Edit(CurrentUser);
                MessageBox.Show("Clave actualizada!");

                Users = userRepository.GetByAll();

            }
            catch (Exception e)
            {
                MessageBox.Show("No se ha podido registrar, " + e.Message);
                //throw new Exception("No se ha podido registrar");
            }
        }
        private bool CanExecuteAddUserCommand(object obj)
        {
            bool validData;
            if (string.IsNullOrWhiteSpace(Name) || Id.Length < 3 || Name.Length < 3 || Lastname.Length < 3 || Password.Length < 3)
            {
               // MessageBox.Show("Cada campo debe contener al menos 3 caracteres");
                validData = false;
            }
            else if (Type == null){
                //MessageBox.Show("No se ha seleccionado el tipo de usuario");
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
                IUserRepository userRepository = new UserRepository();
                switch (Type.Name)
                {
                    case "Administrador":
                        UserModel admin = new UserModel();
                        admin.Id = Id;
                        admin.Name = Name;
                        admin.LastName = Lastname;
                        admin.Password = Password;
                        admin.Type = "Administrador";
                        userRepository.Add(admin);
                        break;

                    case "Cajero":
                        UserModel cashier = new UserModel();
                        cashier.Id = Id;
                        cashier.Name = Name;
                        cashier.LastName = Lastname;
                        cashier.Password = Password;
                        cashier.Type = "Cajero";
                        userRepository.Add(cashier);
                        MessageBox.Show("Usuario registrado");
                        break;
                }
                Users = userRepository.GetByAll();
                
            }
            catch (Exception e)
            {
                MessageBox.Show("No se ha podido registrar, "+e.Message);
                //throw new Exception("No se ha podido registrar");
            }
        }


    } 


}
