﻿using AgroFamily.Model;
using AgroFamily.Repositories;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace AgroFamily.ViewModel
{
    public class CashRegisterViewModel : ViewModelBase
    {
        //Repositories
        private IProductRepository _productRepository;
        private ISaleRepository _saleRepository;
        private ISaleProductRepository _saleProductRepository;
        public IProductRepository ProductRepository { get => _productRepository; set { _productRepository = value; OnPropertyChanged(nameof(ProductRepository)); } }
        public ISaleRepository SaleRepository { get => _saleRepository; set { _saleRepository = value; OnPropertyChanged(nameof(SaleRepository)); } }
        public ISaleProductRepository SaleProductRepository { get => _saleProductRepository; set { _saleProductRepository = value; OnPropertyChanged(nameof(SaleProductRepository)); } }

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
        public ICommand AddProductCommand { get; }
        public ICommand RemoveProductCommand { get; }
        public ICommand PayCommand { get; }


        public CashRegisterViewModel()
        {
            ProductRepository = new ProductRepository();
            SaleRepository = new SaleRepository();
            SaleProductRepository = new SaleProductRepository();
            UserRepository = new UserRepository();
            LoadCurrentUserData();
            TotalPriceDay = 0;
            ObservableCollection<SaleModel> saleModels = SaleRepository.GetByDay(DateOnly.FromDateTime(DateTime.Now));
            for (int i = 0; i < saleModels.Count; i++)
            {
                TotalPriceDay += saleModels[i].total;
            }
            Products = ProductRepository.GetByAll();
            OverflowQuantityVisibility = Visibility.Collapsed;
            SaleProducts = new ObservableCollection<SaleProductModel>();

            //Initialize Command
            AddProductCommand = new ViewModelCommand(ExecuteAddProductCommand, CanExecuteAddProductCommand);
            RemoveProductCommand = new ViewModelCommand(ExecuteRemoveProductCommand, CanExecuteRemoveProductCommand);
            PayCommand = new ViewModelCommand(ExecutePayCommand, CanExecutePayCommand);
            TextSize = 3;
            TitleSize = 10;//Se define con 10 menos por que al inicializar el border se le suman 10 automaticamente dejandolo en 20
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
            if (SaleProducts.Count > 0)
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
            SaleModel sale = new SaleModel()
            {
                id_vendedor=UserAccount.UserName,
                SaleDate = DateTime.Now,
                total = TotalPrice
            };
            SaleRepository.Add(sale);
            for (int i = 0; i < SaleProducts.Count; i++)
            {
                SaleProducts[i].SaleId = sale.Id;
                SaleProductRepository.Add(SaleProducts[i]);
                ProductModel product = ProductRepository.GetById(SaleProducts[i].ProductId);
                product.Stock -= SaleProducts[i].Count;
                ProductRepository.Edit(product);
            }
            TotalPriceDay += TotalPrice;
            TotalPrice = 0;
            SaleProducts.Clear();
            Products = ProductRepository.GetByAll();
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
                validData = false;
            }
            return validData;
        }

        private void ExecuteAddProductCommand(object obj)
        {
            SaleProducts.Add(new SaleProductModel()
            {
                ProductId = CurrentProduct.Id,
                Count = CurrentQuantityProduct,
                Name = CurrentProduct.Name,
                Amount = CurrentProduct.Price * CurrentQuantityProduct
            });
            TotalPrice += CurrentProduct.Price * CurrentQuantityProduct;
        }
    }
}