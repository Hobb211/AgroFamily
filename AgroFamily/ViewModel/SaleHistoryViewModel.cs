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
        private DateTime _startingDate;
        private DateTime _endingDate;
        private SaleModel _currentSale;
        private string _sellerName;
        private bool _idSearching;
        private bool _dateSearching;
        private readonly ObservableCollection<SaleModel> _historicSales;
        private ObservableCollection<SaleProductModel> _productsOfSale;

        //Properties
        public string SellerID { get => _sellerID; set { _sellerID = value; OnPropertyChanged(nameof(SellerID)); } }
        public string SaleID { get => _saleID; set { _saleID = value; OnPropertyChanged(nameof(SaleID)); } }
        public DateTime StartingDate { get => _startingDate; set { _startingDate = value; OnPropertyChanged(nameof(StartingDate)); } }
        public DateTime EndingDate { get => _endingDate; set { _endingDate = value; OnPropertyChanged(nameof(EndingDate)); } }
        public SaleModel CurrentSale { get => _currentSale; set { _currentSale = value; OnPropertyChanged(nameof(CurrentSale)); } }
        public IEnumerable<SaleModel> HistoricSales => _historicSales;
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
            bool betweenDateRange;
            bool isToday;

            //Para poder buscar una venta en el historial, se asegura que el vendedor y/o la venta exista en el historial
            if (SearchByID)
            {
                return !string.IsNullOrEmpty(SellerID) && !string.IsNullOrEmpty(SaleID);
            }

            //Respecto al rango de fechas se deben verificar las siguientes condiciones:
            //Fecha final no sea anterior a la fecha de inicio
            //Fecha de inicio y final no sea posterior al dia en curso
            //Si ambas fechas son el dia actual
            if (SearchByDates)
            {
                betweenDateRange = EndingDate.CompareTo(StartingDate) >= 0 || (EndingDate.CompareTo(DateTime.Today) < 0 && StartingDate.CompareTo(DateTime.Today) < 0);
                isToday = EndingDate.CompareTo(DateTime.Today) == 0 && StartingDate.CompareTo(DateTime.Today) == 0;
                return betweenDateRange || isToday;
            }
            return false;
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
                sales_aux = new SaleRepository().GetByDateRange(DateOnly.FromDateTime(StartingDate), DateOnly.FromDateTime(EndingDate));
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
