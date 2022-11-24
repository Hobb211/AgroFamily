using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//referencia al ensamblado de componentes
using System.ComponentModel;
using AgroFamily.Model;
using AgroFamily.Repositories;
using System.Threading;
using System.Windows.Input;
using System.Windows;

namespace AgroFamily.ViewModel
{
    //Creamos una clase abstracta para que solo sea usada a traves de la herencia
    //Creamos e implementamos la interfaz notificar cambios de propiedad 
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        private UserAccountModel _userAccount;
        private IUserRepository _userRepository;

        public UserAccountModel UserAccount { get => _userAccount; set => _userAccount = value; }
        public IUserRepository UserRepository { get => _userRepository; set => _userRepository = value; }
        public double TitleSize { get => _titleSize; set { _titleSize = value; OnPropertyChanged(nameof(TitleSize)); } }
        public double TextSize { get => _textSize; set { _textSize = value; OnPropertyChanged(nameof(TextSize)); } }
        public double ButtonWidth1 { get => _buttonWidth1; set { _buttonWidth1 = value; OnPropertyChanged(nameof(ButtonWidth1)); } }
        public double ButtonWidth2 { get => _buttonWidth2; set { _buttonWidth2 = value; OnPropertyChanged(nameof(ButtonWidth2)); } }
        public double ButtonHeight1 { get => _buttonHeight1; set { _buttonHeight1 = value; OnPropertyChanged(nameof(ButtonHeight1)); } }
        public double ButtonHeight2 { get => _buttonHeight2; set { _buttonHeight2 = value; OnPropertyChanged(nameof(ButtonHeight2)); } }

        //FontSize
        private double _titleSize;
        private double _textSize;
        //ButtonSize
        private double _buttonWidth1;
        private double _buttonWidth2;
        private double _buttonHeight1;
        private double _buttonHeight2;

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void LoadCurrentUserData()
        {
            UserAccount = new UserAccountModel();
            var user = UserRepository.GetById(Thread.CurrentPrincipal.Identity.Name);//Pasar un UserName por el principal
            if (user != null)
            {
                UserAccount.UserName = user.Id;
                UserAccount.Name = user.Name;
                UserAccount.LastName = user.LastName;
                UserAccount.Role = user.Type;
            }
        }

        public void ChangeSizeFont()
        {
            if ((bool)Application.Current.Properties["IsViewMinimize"])
            {
                TextSize += 10;
                TitleSize += 10;
                ButtonHeight1 += 20;
                ButtonHeight2 += 20;
                ButtonWidth1 +=20;
                ButtonWidth2 +=20;
            }
            else
            {
                TextSize -= 10;
                TitleSize -= 10;
                ButtonHeight1 -= 20;
                ButtonHeight2 -= 20;
                ButtonWidth1 -= 20;
                ButtonWidth2 -= 20;
            }
        }

    }
}
