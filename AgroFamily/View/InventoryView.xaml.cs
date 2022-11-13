using AgroFamily.Model;
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
    /// Interaction logic for InventoryView.xaml
    /// </summary>
    public partial class InventoryView : UserControl
    {
        public InventoryView()
        {
            InitializeComponent();
        }

        private void membersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DataGridRow_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            var row = sender as DataGridRow;
            var article = row.DataContext as ArticleModel;

            MessageBox.Show($"El articulo clickeado es {article.Name}!");
            //articlesDataGrid.IsReadOnly = false;
            //articlesDataGrid.CurrentCell = new DataGridCellInfo((sender as Button).DataContext, articlesDataGrid.Columns[0]);
            //articlesDataGrid.BeginEdit();
        }


    }
}
