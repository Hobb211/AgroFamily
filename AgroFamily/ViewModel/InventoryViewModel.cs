using AgroFamily.Model;
using AgroFamily.Repositories;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroFamily.ViewModel
{
    public class InventoryViewModel : ViewModelBase
    {
        public ObservableCollection<ProductModel> _products;
        public ObservableCollection<SuppliesModel> _supplies;

        public ObservableCollection<ProductModel> Products { get => _products; set => _products = value; }
        public ObservableCollection<SuppliesModel> Supplies { get => _supplies; set => _supplies = value; }

        public InventoryViewModel()
        {
            IProductRepository productRepository = new ProductRepository();
            Products = productRepository.GetByAll3();

            ISuppliesRepository suppliesRepository = new SuppliesRepository();
            Supplies = suppliesRepository.GetByAll3();







        }

    }
}
