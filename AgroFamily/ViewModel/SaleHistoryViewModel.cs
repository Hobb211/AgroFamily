using AgroFamily.Exceptions;
using AgroFamily.Model;
using AgroFamily.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AgroFamily.ViewModel
{
    public class SaleHistoryViewModel : ViewModelBase
    {

        //Fields
        private string _sellerID;
        private DateTime _startingDate = DateTime.Today;
        private DateTime _endingDate = DateTime.Today;
        private SaleModel _currentSale;
        private string _currentSaleSellerName;
        private bool _idSearching = false;
        private bool _dateSearching = false;
        private ObservableCollection<SaleModel> _historicSales;
        private ObservableCollection<SaleProductModel> _productsOfSale;
        private long _salesOfPeriod;
        private string _earningOfPeriod;
        private string _currentSaleTotal;
        private string _currentSaleDate;
        private Visibility _isID;
        private Visibility _isDates;


        //Properties
        public string SellerID { get => _sellerID; set { _sellerID = value; OnPropertyChanged(nameof(SellerID)); } }
        public DateTime StartingDate { get => _startingDate; set { _startingDate = value; OnPropertyChanged(nameof(StartingDate)); } }
        public DateTime EndingDate { get => _endingDate; set { _endingDate = value; OnPropertyChanged(nameof(EndingDate)); } }
        public ObservableCollection<SaleModel> HistoricSales { get => _historicSales; set { _historicSales = value; OnPropertyChanged(nameof(HistoricSales)); } }
        public bool SearchByID { get => _idSearching; set { _idSearching = value; OnPropertyChanged(nameof(SearchByID)); } }
        public bool SearchByDates { get => _dateSearching; set { _dateSearching = value; OnPropertyChanged(nameof(SearchByDates)); } }
        public long SalesOfPeriod { get => _salesOfPeriod; set { _salesOfPeriod = value; OnPropertyChanged(nameof(SalesOfPeriod)); } }
        public string EarningOfPeriod { get => _earningOfPeriod; set { _earningOfPeriod = value; OnPropertyChanged(nameof(EarningOfPeriod)); } }
        public string CurrentSaleTotal { get => _currentSaleTotal; set { _currentSaleTotal = value; OnPropertyChanged(nameof(CurrentSaleTotal)); } }
        public string CurrentSaleDate { get => _currentSaleDate; set { _currentSaleDate = value; OnPropertyChanged(nameof(CurrentSaleDate)); } }
        public SaleModel CurrentSale 
        { 
            get => _currentSale; 
            set 
            {
                _currentSale = value;
                OnPropertyChanged(nameof(CurrentSale));
                ISaleProductRepository saleProductRepository = new SaleProductRepository();
                if(CurrentSale!=null)
                {
                    ProductsOfSale = saleProductRepository.GetBySale(CurrentSale.Id);
                    CurrentSaleSellerName = CurrentSale.salerName;
                    var nfi = new NumberFormatInfo()
                    {
                        NumberDecimalDigits = 0,
                        NumberGroupSeparator = "."
                    };
                    CurrentSaleTotal = CurrentSale.total.ToString("N", nfi);
                    CurrentSaleDate = CurrentSale.SaleDate.ToShortDateString();
                }
                else
                {
                    ProductsOfSale = null;
                    CurrentSaleSellerName = "";
                }
            } 
        }

        //La propiedad vendedor que será mostrada en la vista
        //debe ser obtenida de la base de datos de usuarios
        public string CurrentSaleSellerName
        {
            get
            {
                return _currentSaleSellerName;
            }
            set
            {
                _currentSaleSellerName = value;
                OnPropertyChanged(nameof(CurrentSaleSellerName));
            }
        }
        public ObservableCollection<SaleProductModel> ProductsOfSale
        {
            get
            {
                return _productsOfSale;
            }
            set
            {
                _productsOfSale = value;
                OnPropertyChanged(nameof(ProductsOfSale));
            }
        }

        public Visibility IsID
        {
            get
            {
                return _isID;
            }
            set
            {
                _isID = value;
                OnPropertyChanged(nameof(IsID));
                if (SearchByID)
                {
                    IsID = Visibility.Visible;
                }
                else
                {
                    IsID = Visibility.Collapsed;
                }
            }
        }

        public Visibility IsDates
        {
            get
            {
                return _isDates;
            }
            set
            {
                _isDates = value;
                OnPropertyChanged(nameof(IsDates));
                if (SearchByDates)
                {
                    IsDates = Visibility.Visible;
                }
                else
                {
                    IsDates = Visibility.Collapsed;
                }
            }
        }
        //Commands
        public ICommand SearchSaleCommand { get; }
        public ICommand ExportCsvCommand { get; }

        public SaleHistoryViewModel()
        {
            HistoricSales = new ObservableCollection<SaleModel>();
            ProductsOfSale = new ObservableCollection<SaleProductModel>();

            //Initialize Command
            SearchSaleCommand = new ViewModelCommand(ExecuteSearchSaleCommand, CanExecuteSearchSaleCommand);
            ExportCsvCommand = new ViewModelCommand(ExecuteExportCsvCommand, CanExecuteExportCsvCommand);

            TextSizeChange = 10;
            ButtonChangeSizeH = 20;
            ButtonChangeSizeW = 20;

            if ((bool)Application.Current.Properties["IsViewMinimize"])
            {
                TextSize = 3;
                TitleSize = 14;
                ButtonHeight1 = 10;
                ButtonWidth1 = 60;
            }
            else
            {
                TextSize = 33;
                TitleSize = 44;
                ButtonHeight1 = 70;
                ButtonWidth1 = 120;
            }
        }

        private void ExecuteExportCsvCommand(object obj)
        {
            throw new NotImplementedException();
        }

        private bool CanExecuteExportCsvCommand(object obj)
        {
            return HistoricSales.Any();
        }

        private bool CanExecuteSearchSaleCommand(object obj)
        {
            //Para poder buscar una venta en el historial, se asegura que el vendedor y/o la venta exista en el historial
            if (_idSearching)
            {
                return !string.IsNullOrEmpty(SellerID);
            }

            //Respecto al rango de fechas se deben verificar las siguientes condiciones:
            //Fecha final no sea anterior a la fecha de inicio
            //Fecha de inicio y final no sea posterior al dia en curso
            //Si ambas fechas son el dia actual
            if (_dateSearching)
            {
                bool betweenDateRange = EndingDate.CompareTo(StartingDate) >= 0 || (EndingDate.CompareTo(DateTime.Today) < 0 && StartingDate.CompareTo(DateTime.Today) < 0);
                bool isToday = EndingDate.CompareTo(DateTime.Today) == 0 && StartingDate.CompareTo(DateTime.Today) == 0;
                return betweenDateRange || isToday;
            }
            return false;
        }

        private void ExecuteSearchSaleCommand(object obj)
        {
            CurrentSale = null;
            UserModel user = null;
            ObservableCollection<SaleModel> sales_aux = null;
            //Primero se comprueba que tipo de busqueda se desea hacer
            //Si se desea buscar mediante un rango de fechas
            if (SearchByDates)
            {
                try
                {
                    sales_aux = new SaleRepository().GetByDateRange(DateOnly.FromDateTime(StartingDate), DateOnly.FromDateTime(EndingDate));
                }
                catch (SaleConflictException ex)
                {
                    sales_aux = new ObservableCollection<SaleModel>();
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            //Si desea buscar por identificadores
            else if (SearchByID)
            {
                //Primero se busca en la base de datos
                //Que hayan ventas, en general
                //Si no lanza excepcion
                try
                {
                    sales_aux = new SaleRepository().GetAll();
                }
                catch (SaleConflictException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                //Luego que el vendedor solicitado mediante su id exista
                //Si no lo encuentra debe lanzar excepcion
                try
                {
                    user = new UserRepository().GetById(SellerID);
                    sales_aux = (new SaleRepository().GetBySeller(user.Id));

                }
                catch (UserConflictException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            long earning = 0;
            foreach (SaleModel sale in sales_aux)
            {
                earning += sale.total;
            }
            var nfi = new NumberFormatInfo()
            {
                NumberDecimalDigits = 0,
                NumberGroupSeparator = "."
            };
            EarningOfPeriod = earning.ToString("N", nfi);
            SalesOfPeriod = sales_aux.Count;
            HistoricSales = sales_aux;
        }
    }
}

