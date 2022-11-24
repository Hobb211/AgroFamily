using AgroFamily.Exceptions;
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
    public class SaleHistoryViewModel : ViewModelBase
    {

        //Fields
        private string _sellerID;
        private string _saleID;
        private DateTime _startingDate = DateTime.Today;
        private DateTime _endingDate = DateTime.Today;
        private SaleModel _currentSale;
        private string _sellerName;
        private bool _idSearching = false;
        private bool _dateSearching = false;
        private ObservableCollection<SaleModel> _historicSales;
        private ObservableCollection<SaleProductModel> _productsOfSale;

        //Properties
        public string SellerID { get => _sellerID; set { _sellerID = value; OnPropertyChanged(nameof(SellerID)); } }
        public string SaleID { get => _saleID; set { _saleID = value; OnPropertyChanged(nameof(SaleID)); } }
        public DateTime StartingDate { get => _startingDate; set { _startingDate = value; OnPropertyChanged(nameof(StartingDate)); } }
        public DateTime EndingDate { get => _endingDate; set { _endingDate = value; OnPropertyChanged(nameof(EndingDate)); } }
        public SaleModel CurrentSale { get => _currentSale; set { _currentSale = value; OnPropertyChanged(nameof(CurrentSale)); } }
        public ObservableCollection<SaleModel> HistoricSales { get => _historicSales; set { _historicSales = value; OnPropertyChanged(nameof(HistoricSales)); } }
        public bool SearchByID { get => _idSearching; set { _idSearching = value; OnPropertyChanged(nameof(SearchByID)); } }
        public bool SearchByDates { get => _dateSearching; set { _dateSearching = value; OnPropertyChanged(nameof(SearchByDates)); } }

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
                    user = userRepository.GetById(_currentSale.id_vendedor);
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
                catch (NullReferenceException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (Exception e)
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
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
                OnPropertyChanged(nameof(ProductsOfSale));
            }
        }

        //Commands
        public ICommand SearchSaleCommand { get; }

        public SaleHistoryViewModel()
        {
            HistoricSales = new ObservableCollection<SaleModel>();
            _productsOfSale = new ObservableCollection<SaleProductModel>();

            //Initialize Command
            SearchSaleCommand = new ViewModelCommand(ExecuteSearchSaleCommand, CanExecuteSearchSaleCommand);
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
                }
                catch (UserConflictException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }


                //Primeramente si se desea buscar por id de vendedor solamente
                if (!string.IsNullOrEmpty(SellerID) && string.IsNullOrEmpty(SaleID.ToString()))
                {
                    try
                    {
                        sales_aux = (new SaleRepository().GetBySeller(user.Id));
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

                //Luego si se desea buscar solamente por el id de venta
                else if (string.IsNullOrEmpty(SellerID) && !string.IsNullOrEmpty(SaleID.ToString()))
                {
                    SaleModel sale = null;
                    try
                    {
                        sale = new SaleRepository().GetById(int.Parse(SaleID));
                        sales_aux.Add(sale);
                    }
                    catch (SaleConflictException e)
                    {
                        MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

                //Finalmente si se desean usar ambos parametros para realizar la busqueda
                else if (!string.IsNullOrEmpty(SellerID) && !string.IsNullOrEmpty(SaleID.ToString()))
                {
                    try
                    {
                        sales_aux.Add(new SaleRepository().GetBySellerIDSaleID(SellerID, int.Parse(SaleID)));
                    }
                    catch (SaleConflictException ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            HistoricSales = sales_aux;
            MessageBox.Show(_historicSales[1].total.ToString(), "id", MessageBoxButton.OK);
        }
    }
}

