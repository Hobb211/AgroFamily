﻿using AgroFamily.Model;
using AgroFamily.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace AgroFamily.ViewModel
{
    public class AddItemsViewModel : ViewModelBase
    {
        //Fields
        private string _name;
        private int _count;
        private TypeInventoryModel _type;
        private int _price;
        private Visibility _priceVisibility;
        private ObservableCollection<TypeInventoryModel> _typeInventory;
        private ObservableCollection<ArticleModel> _articles;

        //Propierties
        public string Name { get => _name; set { _name = value; OnPropertyChanged(nameof(Name)); } }
        public int Count { get => _count; set { _count = value; OnPropertyChanged(nameof(Count)); } }
        public int Price { get => _price; set { _price = value; OnPropertyChanged(nameof(Price)); } }
        public Visibility PriceVisibility { get => _priceVisibility; set { _priceVisibility = value; OnPropertyChanged(nameof(PriceVisibility)); } }
        public TypeInventoryModel Type 
        { 
            get => _type; 
            set 
            { 
                _type = value; 
                OnPropertyChanged(nameof(Type)); 
                if (Type.Name.Equals("Producto"))
                {
                    PriceVisibility = Visibility.Visible;
                }
                else
                {
                    PriceVisibility = Visibility.Collapsed;
                }
            } 
        }
        public ObservableCollection<TypeInventoryModel> TypeInventory { get => _typeInventory; set => _typeInventory = value; }
        public ObservableCollection<ArticleModel> Articles { get => _articles; set { _articles = value; OnPropertyChanged(nameof(Articles)); } }

        //Commands
        public ICommand AddItemCommand { get;}
        

        //Constructor
        public AddItemsViewModel()
        {
            Count = 0;
            AddItemCommand = new ViewModelCommand(ExecuteAddItemCommand, CanExecuteAddItemCommand);
            ITypeInventoryRepository typeInventoryRepository = new TypeInventoryRepository();
            TypeInventory = typeInventoryRepository.GetByAll();
            IProductRepository productRepository = new ProductRepository();
            ISuppliesRepository suppliesRepository = new SuppliesRepository();
            Articles = suppliesRepository.GetByAllArticles();
            ObservableCollection<ArticleModel> articlesObservable = new ObservableCollection<ArticleModel>();
            Articles = new ObservableCollection<ArticleModel>(suppliesRepository.GetByAllArticles().Concat(productRepository.GetByAllArticles()));

            TextSizeChange = 10;
            ButtonChangeSizeH = 20;
            ButtonChangeSizeW = 80;
            if ((bool)Application.Current.Properties["IsViewMinimize"])
            {
                TextSize = 3;
                TitleSize = 10;
                ButtonHeight1 = 20;
                ButtonWidth1 = 80;
            }
            else
            {
                TextSize = 33;
                TitleSize = 40;
                ButtonHeight1 = 80;
                ButtonWidth1 = 320;
            }
        }

        private bool CanExecuteAddItemCommand(object obj)
        {
            bool validData;
            if (string.IsNullOrWhiteSpace(Name) || Name.Length < 3 || Type == null )
            {
                validData = false;
            }
            else
            {
                if (Type.Name.Equals("Producto"))
                {
                    if (Price > 0)
                    {
                        validData = true;
                    }
                    else
                    {
                        validData = false;
                    }   
                }
                else { validData = true; }
            }
            return validData;
        }

        private void ExecuteAddItemCommand(object obj)
        {
            try
            {
                IProductRepository productRepository = new ProductRepository();
                ISuppliesRepository suppliesRepository = new SuppliesRepository();

                switch (Type.Name)
                {
                    case "Producto":
                        ProductModel product = new ProductModel();
                        product.Name = Name;
                        product.Price = Price;
                        product.Stock = Count;
                        product.Type = Type.Name;
                        productRepository.Add(product);
                        break;

                    case "Suministro":
                        SuppliesModel supplies = new SuppliesModel();
                        supplies.Name = Name;
                        supplies.Stock = Count;
                        supplies.Type= Type.Name;
                        suppliesRepository.Add(supplies);
                        break;
                }
                Name = "";
                Price= 0;
                Count = 0;
                Articles = new ObservableCollection<ArticleModel>(suppliesRepository.GetByAllArticles().Concat(productRepository.GetByAllArticles()));
                MessageBox.Show("Se ha añadido el "+Type.Name+" con exito");
            }
            catch
            {
                MessageBox.Show("Un error a ocurrido");
            }
            
        }

        
    }
}
