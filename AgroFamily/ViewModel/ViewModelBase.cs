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

        //FontSize
        private double _titleSize;
        private double _textSize;

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
            }
            else
            {
                TextSize -= 10;
                TitleSize -= 10;
            }
        }

    }
}
