using AgroFamily.Model;
using AgroFamily.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AgroFamily.View
{
    /// <summary>
    /// Lógica de interacción para CashRegisterView.xaml
    /// </summary>
    public partial class CashRegisterView : UserControl
    {
        public CashRegisterView()
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

        private void button_search_TextChanged_Cash(object sender, TextChangedEventArgs e)
        {
            CashRegisterViewModel viewModel = (CashRegisterViewModel)this.DataContext;
            viewModel.ExecuteGetCoincidencesCash();
        }

        private void productsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ItemAmmount.Clear();
            ItemAmmount.Focus();
        }

        private void ItemAmmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                CashRegisterViewModel viewModel = (CashRegisterViewModel)this.DataContext;
                viewModel.AddProductCommand.Execute(viewModel);
            }
        }
    }
}
