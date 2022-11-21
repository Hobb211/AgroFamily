﻿using AgroFamily.Model;
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
    public class SaleHistoryViewModel : ViewModelBase
    {

        //Fields
        private string _sellerID;
        private string _saleID;
        private DateOnly _startingDate = DateOnly.FromDateTime(DateTime.Today);
        private DateOnly _endingDate = DateOnly.FromDateTime(DateTime.Today);
        private SaleModel _currentSale;
        private string _sellerName;
        private readonly ObservableCollection<SaleModel> _historicSales;
        private ObservableCollection<SaleProductModel> _productsOfSale;

        //Properties
        public string SellerID { get => _sellerID; set { _sellerID = value; OnPropertyChanged(nameof(SellerID)); } }
        public string SaleID { get => _saleID; set { _saleID = value; OnPropertyChanged(nameof(SaleID)); } }
        public DateOnly StartingDate { get => _startingDate; set { _startingDate = value; OnPropertyChanged(nameof(StartingDate)); } }
        public DateOnly EndingDate { get => _endingDate; set { _endingDate = value; OnPropertyChanged(nameof(EndingDate)); } }
        public SaleModel CurrentSale { get => _currentSale; set { _currentSale = value; OnPropertyChanged(nameof(CurrentSale)); } }
        public IEnumerable<SaleModel> HistoricSales => _historicSales;

        //La propiedad vendedor que será mostrada en la vista
        //debe ser obtenida de la base de datos de usuarios
        public string SellerName
        {
            get
            {
                return _sellerName;
            }
            set
            {
                IUserRepository userRepository = new UserRepository();
                UserModel user = null;
                try
                {
                    user = userRepository.GetById(CurrentSale.id_vendedor);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
                try
                {
                    _sellerName = userRepository.GetSeller(user);
                    OnPropertyChanged(nameof(SellerName));
                }
                catch(NullReferenceException ex)
                {
                    MessageBox.Show(ex.Message);
                }catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
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
                try
                {
                    _productsOfSale = new SaleProductRepository().GetBySale(CurrentSale.Id.ToString());
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.Message);
                }                
                OnPropertyChanged(nameof(ProductsOfSale));
            }
        }

        //public bool OnlyToday { get => _onlyToday; set { _onlyToday = value; OnPropertyChanged(nameof(OnlyToday)); } }
        //public bool DateRange { get => _dateRange; set { _dateRange = value; OnPropertyChanged(nameof(DateRange)); } }

        //Commands
        public ICommand SearchSaleCommand { get; }

        public SaleHistoryViewModel()
        {
            _historicSales = new ObservableCollection<SaleModel>();
            _productsOfSale = new ObservableCollection<SaleProductModel>();
            //Initialize Command
            SearchSaleCommand = new ViewModelCommand(ExecuteSearchSaleCommand, CanExecuteSearchSaleCommand);
        }

        private bool CanExecuteSearchSaleCommand(object obj)
        {
            bool validParameters;
            bool validDates;
            bool betweenDateRange;
            bool isToday;

            //Para poder buscar una venta en el historial, se asegura que el vendedor y/o la venta exista en el historial
            validParameters = !string.IsNullOrEmpty(SellerID) && !string.IsNullOrEmpty(SaleID);

            //Respecto al rango de fechas se deben verificar las siguientes condiciones:
            //Fecha final no sea anterior a la fecha de inicio
            //Fecha de inicio y final no sea posterior al dia en curso
            //Si ambas fechas son el dia actual
            DateOnly Today = DateOnly.FromDateTime(DateTime.Today);
            betweenDateRange = EndingDate.CompareTo(StartingDate) >= 0 || (EndingDate.CompareTo(DateTime.Today) < 0 && StartingDate.CompareTo(DateTime.Today) < 0);
            isToday = EndingDate.CompareTo(Today) == 0 && StartingDate.CompareTo(Today) == 0;
            validDates = betweenDateRange || isToday;

            return validParameters && validDates;
        }

        private void ExecuteSearchSaleCommand(object obj)
        {
            UserModel user = new UserModel();
            ObservableCollection<SaleModel> sales_aux = new ObservableCollection<SaleModel>();

            //Primero se busca en la base de datos el vendedor solicitado mediante su id
            //y el listado de ventas
            //Si no lo encuentra debe lanzar excepcion
            try
            {
                user = new UserRepository().GetById(SellerID);
                sales_aux = new SaleRepository().GetByDateRange(StartingDate, EndingDate);
            }
            catch (Exception ex)
            {
                //Esta messagebox es temporal, solo para prueba de errores
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            //Por cada venta que exista va agregar a la lista las que correspondan al vendedor
            //y a las fechas solicitadas
            foreach (SaleModel sale in sales_aux)
            {
                if (sale.id_vendedor == SellerID)
                {
                    HistoricSales.Append<SaleModel>(sale);
                }
            }
        }
    }
}
