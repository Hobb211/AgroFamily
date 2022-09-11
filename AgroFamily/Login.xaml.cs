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
using SQLite;

namespace AgroFamily
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Ingresar(object sender, RoutedEventArgs e)
        {
            using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
            {
                User user = connection.Find<User>(UserName.Text);
                if(user == null)
                {
                    NickNameWrong.Visibility = Visibility.Visible;
                }
                else
                {
                    NickNameWrong.Visibility = Visibility.Collapsed;
                    if (user.Password.Equals(Password.Password))
                    {
                        Close();
                    }
                    else
                    {
                        PasswordWrong.Visibility = Visibility.Visible;
                    }
                }
            }
        }
    }
}
