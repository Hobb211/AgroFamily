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
        private bool _idSearching;
        private bool _dateSearching;
        private bool _isSellerID = false;
        private bool _isSaleID = false;
        private ObservableCollection<SaleModel> _historicSales;
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
        public bool IsSellerID { get => _isSellerID; set { _isSellerID = value; OnPropertyChanged(nameof(IsSellerID)); } }
        public bool IsSaleID { get => _isSellerID; set { _isSellerID = value; OnPropertyChanged(nameof(IsSaleID)); } }

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
                return !string.IsNullOrEmpty(SellerID) || !string.IsNullOrEmpty(SaleID);
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
            ISaleRepository saleRepository = new SaleRepository();
            ObservableCollection<SaleModel> sales_aux = saleRepository.GetAll();

            //Primero se busca en la base de datos el vendedor solicitado mediante su id
            //y el listado de ventas
            //Si no lo encuentra debe lanzar excepcion
            try
            {
                user = new UserRepository().GetById(SellerID);
            }
            catch (Exception ex)
            {
                //Esta messagebox es temporal, solo para prueba de errores
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            //Primero se comprueba que tipo de busqueda se desea hacer
            //Si desea buscar por identificadores
            if (SearchByID)
            {
                //Primeramente si se desea buscar por id de vendedor solamente
                if (IsSellerID)
                {
                    try
                    {
                        sales_aux = saleRepository.GetBySeller(user.Id);
                    }
                    catch(Exception e)
                    {
                        MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                //Luego si se desea buscar solamente por el id de venta
                else if (IsSaleID)
                {
                    try
                    {
                        sales_aux.Add(saleRepository.GetById(int.Parse(SaleID)));
                    }
                    catch(Exception e)
                    {
                        MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                //Finalmente si se desean usar ambos parametros para realizar la busqueda
                else if (IsSellerID && IsSaleID)
                {
                    foreach(SaleModel saleModel in sales_aux)
                    { 
                        if(saleModel.id_vendedor != user.Id && saleModel.Id != int.Parse(SaleID))
                        {
                            sales_aux.Remove(saleModel);
                        }
                    }
                }
            }
            //O si se desea buscar mediante un rango de fechas
            if(SearchByDates)
            {
                sales_aux= saleRepository.GetByDateRange(DateOnly.FromDateTime(StartingDate), DateOnly.FromDateTime(EndingDate));
            }
            _historicSales = sales_aux;
        }
    }
}
