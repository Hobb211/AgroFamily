using AgroFamily.Model;
using SQLite;
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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using AgroFamily;
using System.Windows.Shapes;
using UserControl = System.Windows.Controls.UserControl;
using AgroFamily.Repositories;
using Binding = System.Windows.Data.Binding;
using AgroFamily.ViewModel;

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


        private void productsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void rentable2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void rentable1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void rentable3_Click(object sender, RoutedEventArgs e)
        {

        }



        private void productProfitabilityDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void button_search_TextChanged(object sender, TextChangedEventArgs e)
        {

            ProductProfitabilityViewModel searchName = (ProductProfitabilityViewModel)this.DataContext;
            searchName.ExecuteShowSearchProduc();
        }
    }
}
