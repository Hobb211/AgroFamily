﻿using System;
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
        public double ButtonChangeSizeH { get => _buttonChangeSizeH; set => _buttonChangeSizeH = value; }
        public double ButtonChangeSizeW { get => _buttonChangeSizeW; set => _buttonChangeSizeW = value; }
        public double TextSizeChange { get => _textSizeChange; set => _textSizeChange = value; }
        public double TextBoxHeight { get => _textBoxHeight; set { _textBoxHeight = value; OnPropertyChanged(nameof(TextBoxHeight)); } }
        public double TextBoxChangeSize { get => _textBoxChangeSize; set => _textBoxChangeSize = value; }

        //FontSize
        private double _titleSize;
        private double _textSize;
        private double _textSizeChange;

        //ButtonSize
        private double _buttonWidth1;
        private double _buttonWidth2;
        private double _buttonHeight1;
        private double _buttonHeight2;
        private double _buttonChangeSizeH;
        private double _buttonChangeSizeW;
        //TextBoxSize
        private double _textBoxHeight;
        private double _textBoxChangeSize;

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
                TextSize += TextSizeChange;
                TitleSize += TextSizeChange;
                ButtonHeight1 += ButtonChangeSizeH;
                ButtonHeight2 += ButtonChangeSizeH;
                ButtonWidth1 +=ButtonChangeSizeW;
                ButtonWidth2 +=ButtonChangeSizeW;
                TextBoxHeight += TextBoxChangeSize;
            }
            else
            {
                TextSize -= TextSizeChange;
                TitleSize -= TextSizeChange;
                ButtonHeight1 -= ButtonChangeSizeH;
                ButtonHeight2 -= ButtonChangeSizeH;
                ButtonWidth1 -= ButtonChangeSizeW;
                ButtonWidth2 -= ButtonChangeSizeW;
                TextBoxHeight-= TextBoxChangeSize;
            }
        }

    }
}
