using AgroFamily.Model;
using AgroFamily.Repositories;
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
        //Fields
        private ObservableCollection<ProductModel> _products;
        private ObservableCollection<SaleProductModel> _saleProducts;
        private SaleModel _saleModel;

        //Propierties
        public ObservableCollection<ProductModel> Products { get => _products; set { _products = value; OnPropertyChanged(nameof(Products)); } }
        public ObservableCollection<SaleProductModel> SaleProducts { get => _saleProducts; set { _saleProducts = value; OnPropertyChanged(nameof(SaleProducts)); } }
        public SaleModel SaleModel { get => _saleModel; set { _saleModel = value; OnPropertyChanged(nameof(SaleModel)); } }

        public CashRegisterViewModel()
        {
            IProductRepository repository = new ProductRepository();
            Products = repository.GetByAll();
        }

        

    }
}