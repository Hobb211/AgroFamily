using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AgroFamily.View;

namespace AgroFamily
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    
    //Para cambiar a la vista que se nececite 
    public partial class App : Application
    {
        protected void ApplicationStart(object sender, StartupEventArgs e)
        {
            var loginView = new LoginView();
            loginView.Show();
            loginView.IsVisibleChanged += (s, ev) =>
            {
                if (loginView.IsVisible == false && loginView.IsLoaded)
                {
                    var mainView = new SaleView();
                    mainView.Show();
                    loginView.Close();
                }
            };
        }
    }
}
