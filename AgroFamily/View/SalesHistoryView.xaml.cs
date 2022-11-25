using AgroFamily.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AgroFamily.View
{
    /// <summary>
    /// Lógica de interacción para SalesHistoryView.xaml
    /// </summary>
    public partial class SalesHistoryView : UserControl
    {
        public SalesHistoryView()
        {
            InitializeComponent();
        }

        private void IDSButton_Checked(object sender, RoutedEventArgs e)
        {
            //Cuando se selecciona la opcion de buscar mediante identificadores
            //Se debe habilitar los campos correspondientes
            SellerIDField.IsEnabled = true;
            SaleIDField.IsEnabled = true;
            //Se debe deshabilitar los campos correspondientes a las fechas
            StartDate.IsEnabled = false;
            EndDate.IsEnabled = false;
        }

        private void DatesButton_Checked(object sender, RoutedEventArgs e)
        {
            //Y cuando se selecciona la opcion para buscar mediante fechas
            //Se debe inhabilitar la opcino para buscar por identificadores
            StartDate.IsEnabled = true;
            EndDate.IsEnabled = true;
            //Deshabilitar y vaciar los campos anteriores
            SellerIDField.IsEnabled = false;
            SaleIDField.IsEnabled = false;

        }

        private void Border_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ViewModelBase viewModel = (ViewModelBase)this.DataContext;
            viewModel.ChangeSizeFont();
        }
    }
}