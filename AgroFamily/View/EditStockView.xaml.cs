using AgroFamily.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Navigation;

namespace AgroFamily.View
{

    public partial class EditStockView : UserControl
    {
        public EditStockView()
        {
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void Border_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ViewModelBase viewModel = (ViewModelBase)this.DataContext;
            viewModel.ChangeSizeFont();
        }

        private void tId_TextChanged(object sender, TextChangedEventArgs e)
        {
            EditStockViewModel searchName = (EditStockViewModel)this.DataContext;
            searchName.ExecuteGetCoincidences();
        }

        private void eliminar_stock_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}


