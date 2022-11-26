using AgroFamily.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using AgroFamily.ViewModel;
using System.Security.Policy;
using System.Diagnostics.Tracing;
using SQLite;
using AgroFamily.Repositories;

namespace AgroFamily.View
{
    /// <summary>
    /// Interaction logic for UserRegisterView.xaml
    /// </summary>
    public partial class UserRegisterView : UserControl
    {
        public UserRegisterView()
        {
            InitializeComponent();
            
        }

        private void membersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Border_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ViewModelBase viewModel = (ViewModelBase)this.DataContext;
            viewModel.ChangeSizeFont();
        }





        //private void LoadDataCommand_CanExecute(object sender, CanExecuteRoutedEventArgs args)
        //{
        //    args.CanExecute = true;
        //}

        //private void LoadDataCommand_Executed(object sender, ExecutedRoutedEventArgs args)
        //{
        //    // TODO: ask the model for the data
        //}
    }

    //public static class CustomCommands
    //{
    //    public static readonly RoutedUICommand LoadData = new RoutedUICommand
    //        (
    //            "LoadData",
    //            "LoadData",
    //            typeof(CustomCommands),
    //            new InputGestureCollection()
    //            {
    //            // allow Ctrl+L to perform this command
    //            new KeyGesture(Key.L, ModifierKeys.Control)
    //            }
    //        );
    //}

}
