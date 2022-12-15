using AgroFamily.Model;
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
        private string _name;
        private IProductRepository _productRepository;
        private ISaleRepository _saleRepository;
        private ISaleProductRepository _saleProductRepository;
        private bool _canAddProduct;
        public string Name { get => _name; set { _name = value; OnPropertyChanged(nameof(Name)); } }

        public IProductRepository ProductRepository { get => _productRepository; set { _productRepository = value; OnPropertyChanged(nameof(ProductRepository)); } }
        public ISaleRepository SaleRepository { get => _saleRepository; set { _saleRepository = value; OnPropertyChanged(nameof(SaleRepository)); } }
        public ISaleProductRepository SaleProductRepository { get => _saleProductRepository; set { _saleProductRepository = value; OnPropertyChanged(nameof(SaleProductRepository)); } }


        //Fields
        private ObservableCollection<ProductModel> _products;
        private ObservableCollection<SaleProductModel> _saleProducts;
        private ProductModel _currentProduct;
        private SaleProductModel _currentSaleProduct;
        private long _currentQuantityProduct;
        private string _currentQuantityProduct1;
        private Visibility _overflowQuantityVisibility;
        private long _totalPrice;
        private string _totalPrice1;
        private long _totalPriceDay;
        private string _totalPriceDay1;
        private string _manualAmmount="";

        //Propierties
        public ObservableCollection<ProductModel> Products { get => _products; set { _products = value; OnPropertyChanged(nameof(Products)); } }
        public ObservableCollection<SaleProductModel> SaleProducts { get => _saleProducts; set { _saleProducts = value; OnPropertyChanged(nameof(SaleProducts)); } }
        public ProductModel CurrentProduct { get => _currentProduct; set { _currentProduct = value; OnPropertyChanged(nameof(CurrentProduct)); } }
        public SaleProductModel CurrentSaleProduct { get => _currentSaleProduct; set { _currentSaleProduct = value; OnPropertyChanged(nameof(CurrentSaleProduct)); } }
        public long CurrentQuantityProduct
        {
            get => _currentQuantityProduct;
            set
            {
                _currentQuantityProduct = value;
                OnPropertyChanged(nameof(CurrentQuantityProduct));
                if (CurrentProduct != null)
                {
                    long cant = 0;//Cantidad del producto seleccionado ya agregada al sale
                    for (int i = 0; i < SaleProducts.Count; i++)
                    {
                        if (SaleProducts[i].ProductId == CurrentProduct.Id)
                        {
                            cant += SaleProducts[i].Count;
                        }
                    }
                    if (value+cant > CurrentProduct.Stock)
                    {
                        OverflowQuantityVisibility = Visibility.Visible;
                        CanAddProduct = false;
                    }
                    else
                    {
                        OverflowQuantityVisibility = Visibility.Collapsed;
                        CanAddProduct = true;
                        if (value == 0) { CanAddProduct= false; }
                    }
                }
            }
        }
        public string CurrentQuantityProduct1 { get => _currentQuantityProduct1; set { _currentQuantityProduct1 = value; try { CurrentQuantityProduct = long.Parse(value); } catch { } OnPropertyChanged(nameof(CurrentQuantityProduct1)); } }
        public Visibility OverflowQuantityVisibility { get => _overflowQuantityVisibility; set { _overflowQuantityVisibility = value; OnPropertyChanged(nameof(OverflowQuantityVisibility)); } }
        public long TotalPrice { get => _totalPrice; set { _totalPrice = value; OnPropertyChanged(nameof(TotalPrice)); } }
        public long TotalPriceDay { get => _totalPriceDay; set { _totalPriceDay = value; OnPropertyChanged(nameof(TotalPriceDay)); } }
        public string ManualAmmount
        {
            get => _manualAmmount;
            set
            {
                _manualAmmount = value;
                OnPropertyChanged(nameof(ManualAmmount));
            }
        }


        //Commands
        public ICommand AddProductCommand { get; }
        public ICommand RemoveProductCommand { get; }
        public ICommand PayCommand { get; }
        public bool CanAddProduct { get => _canAddProduct; set => _canAddProduct = value; }
        public string TotalPrice1 { get => _totalPrice1; set { _totalPrice1 = value; OnPropertyChanged(nameof(TotalPrice1)); } }

        public string TotalPriceDay1 { get => _totalPriceDay1; set { _totalPriceDay1 = value; OnPropertyChanged(nameof(TotalPriceDay1)); } }

        public CashRegisterViewModel()
        {
            ProductRepository = new ProductRepository();
            SaleRepository = new SaleRepository();
            SaleProductRepository = new SaleProductRepository();
            UserRepository = new UserRepository();
            LoadCurrentUserData();
            TotalPriceDay = 0;
            TotalPrice1 = "$0";
            ObservableCollection<SaleModel> saleModels = SaleRepository.GetByDay(DateOnly.FromDateTime(DateTime.Now));
            for (int i = 0; i < saleModels.Count; i++)
            {
                TotalPriceDay += saleModels[i].total;
            }
            TotalPriceDay1 = String.Format("{0:C}", TotalPriceDay);
            Products = ProductRepository.GetByAll();
            OverflowQuantityVisibility = Visibility.Collapsed;
            SaleProducts = new ObservableCollection<SaleProductModel>();

            //Initialize Command
            AddProductCommand = new ViewModelCommand(ExecuteAddProductCommand, CanExecuteAddProductCommand);
            RemoveProductCommand = new ViewModelCommand(ExecuteRemoveProductCommand, CanExecuteRemoveProductCommand);
            PayCommand = new ViewModelCommand(ExecutePayCommand, CanExecutePayCommand);

            //Define los tamaños variables descontando o aumentando el valor dependiendo del estado maximizado o minimizado
            TextSizeChange = 10;
            ButtonChangeSizeH = 20;
            ButtonChangeSizeW = 20;
            TextBoxChangeSize = 15;
            if ((bool)Application.Current.Properties["IsViewMinimize"])
            {
                TextSize = 3;
                TitleSize = 10;
                ButtonHeight1 = 20;
                ButtonWidth1 = 100;
                ButtonHeight2 = 10;
                ButtonWidth2 = 80;
                TextBoxHeight = 1;
            }
            else
            {
                TextSize = 33;
                TitleSize = 40;
                ButtonHeight1 = 80;
                ButtonWidth1 = 160;
                ButtonHeight2 = 70;
                ButtonWidth2 = 140;
                TextBoxHeight = 46;
            } 
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
            TotalPrice1 = String.Format("{0:C}", TotalPrice);
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
                salerName=UserAccount.Name+" "+UserAccount.LastName,
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
            TotalPriceDay1 = String.Format("{0:C}", TotalPriceDay);
            TotalPrice = 0;
            TotalPrice1 = "$0";
            SaleProducts.Clear();
            Products = ProductRepository.GetByAll();
        }

        private bool CanExecuteAddProductCommand(object obj)
        {
            bool validData;
            if (CurrentProduct != null)
            {
                long cant = 0;//Cantidad del producto seleccionado ya agregada al sale
                for(int i = 0; i < SaleProducts.Count; i++)
                {
                    if (SaleProducts[i].ProductId == CurrentProduct.Id)
                    {
                        cant += SaleProducts[i].Count;
                    }
                }
                if (CurrentQuantityProduct > 0 && CurrentProduct.Stock>=cant+CurrentQuantityProduct)
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
            long ammount = CurrentProduct.Price * CurrentQuantityProduct; ;
            if (ManualAmmount != "")
            {
                try
                {
                    ammount = long.Parse(ManualAmmount);
                }
                catch { }
            }
            SaleProducts.Add(new SaleProductModel()
            {
                ProductId = CurrentProduct.Id,
                Count = CurrentQuantityProduct,
                Name = CurrentProduct.Name,
                Amount = ammount
            });
            TotalPrice += ammount;
            TotalPrice1 = String.Format("{0:C}", TotalPrice);
            CurrentQuantityProduct = 0;
            CurrentQuantityProduct1 = "";
            ManualAmmount = "";
        }
        public void ExecuteGetCoincidencesCash()

        {
            IProductRepository productRepository = new ProductRepository();
            Products = productRepository.GetProductCoincidences2(Name);

        }

        private bool CanExecuteRemoveArticle2Command(object obj)
        {
            return true;
        }
    }


}