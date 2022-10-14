using AgroFamily.Model;
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

        //Commands
        public ICommand AddItemCommand { get;}

        //Constructor
        public AddItemsViewModel()
        {
            Count = 0;
            AddItemCommand = new ViewModelCommand(ExecuteAddItemCommand, CanExecuteAddItemCommand);
            ITypeInventoryRepository typeInventoryRepository = new TypeInventoryRepository();
            TypeInventory = typeInventoryRepository.GetByAll();
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
                    validData=true;
                }
                else { validData = true; }
            }
            return validData;
        }

        private void ExecuteAddItemCommand(object obj)
        {
            switch(Type.Name)
            {
                case "Producto":
                    ProductModel product = new ProductModel();
                    product.Name = Name;
                    product.Price = Price;
                    product.Stock = Count;
                    IProductRepository productReposiroty = new ProductRepository();
                    productReposiroty.Add(product);
                    break;

                case "Suministro":
                    SuppliesModel suplies = new SuppliesModel();
                    suplies.Name = Name;
                    suplies.Stock = Count;
                    ISuppliesRepository suppliesRepository = new SuppliesRepository();
                    suppliesRepository.Add(suplies);
                    break;
            }
        }

        
    }
}
