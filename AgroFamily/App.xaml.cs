﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AgroFamily.Repositories;
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
            StartAppRepository startApp = new StartAppRepository();
            Application.Current.Properties["IsViewMinimize"] = true;
            startApp.CreateTable();
            var loginView = new LoginView();
            loginView.Show();
            loginView.IsVisibleChanged += (s, ev) =>
            {
                try
                {
                    if (loginView.IsVisible == false && loginView.IsLoaded)
                    {
                        var mainView = new MainView();
                        mainView.Show();
                        loginView.Close();
                        mainView.IsVisibleChanged += (s, ev) =>
                        {
                            try
                            {
                                if (mainView.IsVisible == false && mainView.IsLoaded)
                                {
                                    ApplicationStart(sender, e);
                                    mainView.Close();
                                }
                            }
                            catch (Exception e)
                            {
                            }
                        };
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            };
        }

    }
}
    
