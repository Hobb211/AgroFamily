using AgroFamily.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroFamily.ViewModel
{
    public class CashRegisterViewModel : ViewModelBase
    {
        private readonly ObservableCollection<ProductModel> _products;

        public IEnumerable<ProductModel> Products => _products;

        public CashRegisterViewModel()
        {
            _products = new ObservableCollection<ProductModel>();
        }

        public void addProductToSale(ProductModel product)
        {
            //Add(ProductModel product);
        }
    }
}