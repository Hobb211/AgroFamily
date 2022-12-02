using AgroFamily.Model;
using AgroFamily.ViewModel;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
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
    /// Lógica de interacción para ProductProfitabilityView.xaml
    /// </summary>
    public partial class ProductProfitabilityView : UserControl
    {
        public ProductProfitabilityView()
        {
            InitializeComponent();
        }

        private void Border_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ViewModelBase viewModel = (ViewModelBase)this.DataContext;
            viewModel.ChangeSizeFont();
        }




        private void button_search_TextChanged(object sender, TextChangedEventArgs e)
        {

            ProductProfitabilityViewModel searchName = (ProductProfitabilityViewModel)this.DataContext;
            searchName.ExecuteShowSearchProduc();
        }
    }
}
