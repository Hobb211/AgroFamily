using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Diagnostics;
using SQLite;

namespace AgroFamily
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        private bool NickNameLength=false;
        private bool PasswordLength=false;
        private bool UserNickNameTaken=false;

        public Register()
        {
            InitializeComponent();
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            User user=new User()
            {
                Id=UserNickName.Text,
                Name=UserName.Text,
                LastName=UserLastName.Text,
                Type=UserType.Text,
                Password=UserPassword.Password,
            };
            
            using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
            {
                connection.CreateTable<User>();
                connection.Insert(user);
            }
            Close();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void UserNickName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (UserNickName.Text.Length > 5)
            {
                NickNameLength = true;
                ConfirmButtonEnabled();
            }
            else
            {
                NickNameLength = false;
                ConfirmButtonEnabled();
            }
            using(SQLiteConnection connection = new SQLiteConnection(App.databasePath))
            {
                User user=connection.Find<User>(UserNickName.Text);
                if (user != null)
                {
                    UserNickNameTaken = false;
                    ConfirmButtonEnabled();
                    NickNameTaken.Visibility = Visibility.Visible;
                }
                else
                {   
                    UserNickNameTaken = true;
                    ConfirmButtonEnabled();
                    NickNameTaken.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void ConfirmButtonEnabled()
        {
            if(NickNameLength && UserNickNameTaken && PasswordLength)
            {
                confirmButton.IsEnabled = true;
            }
            else
            {
                confirmButton.IsEnabled = false;
            }
        }

        private void UserPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if(UserPassword.Password.Length > 4)
            {
                PasswordLength = true;
                ConfirmButtonEnabled();
            }
            else
            {
                PasswordLength = false;
                ConfirmButtonEnabled();
            }
        }
    }
}
