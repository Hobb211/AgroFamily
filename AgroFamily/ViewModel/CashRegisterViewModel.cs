using AgroFamily.Model;
using AgroFamily.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AgroFamily.ViewModel
{
    public class CashRegisterViewModel : ViewModelBase
    {
        //Fields
        private ObservableCollection<ProductModel> _products;
        private ObservableCollection<SaleProductModel> _saleProducts;
        private ProductModel _currentProduct;
        private SaleProductModel _currentSaleProduct;
        private int _currentQuantityProduct;
        private Visibility _overflowQuantityVisibility;
        private int _totalPrice;
        private int _totalPriceDay;

        //Propierties
        public ObservableCollection<ProductModel> Products { get => _products; set { _products = value; OnPropertyChanged(nameof(Products)); } }
        public ObservableCollection<SaleProductModel> SaleProducts { get => _saleProducts; set { _saleProducts = value; OnPropertyChanged(nameof(SaleProducts)); } }
        public ProductModel CurrentProduct { get => _currentProduct; set { _currentProduct = value; OnPropertyChanged(nameof(CurrentProduct)); } }
        public SaleProductModel CurrentSaleProduct { get => _currentSaleProduct; set { _currentSaleProduct = value; OnPropertyChanged(nameof(CurrentSaleProduct)); } }
        public int CurrentQuantityProduct 
        { 
            get => _currentQuantityProduct; 
            set 
            { 
                _currentQuantityProduct = value; 
                OnPropertyChanged(nameof(CurrentQuantityProduct));
                if (CurrentProduct != null)
                {
                    if (value > CurrentProduct.Stock)
                    {
                        OverflowQuantityVisibility = Visibility.Visible;
                    }
                    else
                    {
                        OverflowQuantityVisibility = Visibility.Collapsed;
                    }
                }
            } 
        }
        public Visibility OverflowQuantityVisibility { get => _overflowQuantityVisibility; set { _overflowQuantityVisibility = value; OnPropertyChanged(nameof(OverflowQuantityVisibility)); } }
        public int TotalPrice { get => _totalPrice; set { _totalPrice = value; OnPropertyChanged(nameof(TotalPrice)); } }
        public int TotalPriceDay { get => _totalPriceDay; set { _totalPriceDay = value; OnPropertyChanged(nameof(TotalPriceDay)); } }

        //Commands
        public ICommand AddProductCommand { get;}
        public ICommand RemoveProductCommand { get;}
        public ICommand PayCommand { get;}

        public CashRegisterViewModel()
        {
            TotalPriceDay = 0;
            ObservableCollection<SaleModel> saleModels = new SaleRepository().GetByDay(DateOnly.FromDateTime(DateTime.Now));
            for(int i = 0; i < saleModels.Count; i++)
            {
                TotalPriceDay += saleModels[i].total;
            }
            IProductRepository repository = new ProductRepository();
            Products = repository.GetByAll();
            OverflowQuantityVisibility=Visibility.Collapsed;
            SaleProducts = new ObservableCollection<SaleProductModel>();
            //Initialize Command
            AddProductCommand = new ViewModelCommand(ExecuteAddProductCommand, CanExecuteAddProductCommand);
            RemoveProductCommand = new ViewModelCommand(ExecuteRemoveProductCommand, CanExecuteRemoveProductCommand);
            PayCommand = new ViewModelCommand(ExecutePayCommand, CanExecutePayCommand);
        }

        private bool CanExecuteRemoveProductCommand(object obj)
        {
            bool validData;
            if (CurrentSaleProduct != null)
            {
                validData = true;
            }
            else
            {
                validData = false;
            }
            return validData;
        }

        private void ExecuteRemoveProductCommand(object obj)
        {
            TotalPrice -= CurrentSaleProduct.Amount;
            SaleProducts.Remove(CurrentSaleProduct);
        }

        private bool CanExecutePayCommand(object obj)
        {
            bool validData;
            if(SaleProducts.Count > 0)
            {
                validData = true;
            }
            else
            {
                validData = false;
            }
            return validData;
        }

        private void ExecutePayCommand(object obj)
        {
            ISaleProductRepository saleProductRepository = new SaleProductRepository();
            ISaleRepository saleRepository = new SaleRepository();
            IProductRepository productRepository = new ProductRepository();
            SaleModel sale=new SaleModel() 
            { 
                dateTime = DateTime.Now,
                total=TotalPrice
            };
            saleRepository.Add(sale);
            for (int i = 0; i < SaleProducts.Count; i++)
            {
                SaleProducts[i].SaleId = sale.Id;
                saleProductRepository.Add(SaleProducts[i]);
                ProductModel product=productRepository.GetById(SaleProducts[i].ProductId);
                product.Stock -= SaleProducts[i].Count;
                productRepository.Edit(product);
            }
            TotalPriceDay += TotalPrice;
            TotalPrice = 0;
            SaleProducts.Clear();
            Products = productRepository.GetByAll();
        }

        private bool CanExecuteAddProductCommand(object obj)
        {
            bool validData;
            if (CurrentProduct != null)
            {
                if (CurrentQuantityProduct > 0 && CurrentProduct.Stock >= CurrentQuantityProduct)
                {
                    validData = true;
                }
                else
                {
                    validData = false;
                }
            }
            else
            {
                validData= false;
            }
            return validData;
        }

        private void ExecuteAddProductCommand(object obj)
        {
            SaleProducts.Add(new SaleProductModel() 
            { 
                ProductId=CurrentProduct.Id,
                Count=CurrentQuantityProduct,
                Name=CurrentProduct.Name,
                Amount=CurrentProduct.Price*CurrentQuantityProduct
            });
            TotalPrice+=CurrentProduct.Price*CurrentQuantityProduct;
        }
    }
}