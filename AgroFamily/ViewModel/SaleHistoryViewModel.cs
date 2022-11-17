using AgroFamily.Model;
using AgroFamily.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AgroFamily.ViewModel
{
    public class SaleHistoryViewModel : ViewModelBase
    {

        //Fields
        private string _sellerID;
        private string _saleID;
        private DateOnly _startingDate;
        private DateOnly _endingDate;
        private SaleModel _currentSale;
        private bool _onlyToday;
        private bool _dateRange;
        public ObservableCollection<SaleModel> _salesOfSearch;

        //Properties
        public string SellerID { get => _sellerID; set { _sellerID = value; OnPropertyChanged(nameof(SellerID)); } }
        public string SaleID { get => _saleID; set { _saleID = value; OnPropertyChanged(nameof(SaleID)); } }
        public DateOnly StartingDate { get => _startingDate; set { _startingDate = value; OnPropertyChanged(nameof(StartingDate)); } }
        public DateOnly EndingDate { get => _endingDate; set { _endingDate = value; OnPropertyChanged(nameof(EndingDate)); } }
        public SaleModel CurrentSale { get => _currentSale; set { _currentSale = value; OnPropertyChanged(nameof(CurrentSale)); } }
        //public bool OnlyToday { get => _onlyToday; set { _onlyToday = value; OnPropertyChanged(nameof(OnlyToday)); } }
        //public bool DateRange { get => _dateRange; set { _dateRange = value; OnPropertyChanged(nameof(DateRange)); } }

        //Commands
        public ICommand SearchSaleCommand { get; }

        public SaleHistoryViewModel()
        {
            _salesOfSearch = new ObservableCollection<SaleModel>();


            //Initialize Command
            SearchSaleCommand = new ViewModelCommand(ExecuteSearchSaleCommand, CanExecuteSearchSaleCommand);
        }

        private bool CanExecuteSearchSaleCommand(object obj)
        {
            bool validParameters = false;
            bool validDates = false;

            //Para poder buscar una venta los id de venta e id de vendedor deben ser no nulos
            if (SellerID != null && SaleID != null)
            {
                validParameters = true;
            }

            //Respecto al rango de fechas se deben verificar las siguientes condiciones:
            //Fecha final no sea anterior a la fecha de inicio
            //Fecha de inicio y final no sea posterior al dia en curso
            //Si ambas fechas son el dia actual
            if (EndingDate.CompareTo(DateTime.Today) == 0 && StartingDate.CompareTo(DateTime.Today) == 0)
            {
                validDates = true;
            }

            else if(EndingDate.CompareTo(StartingDate) > 0 && EndingDate.CompareTo(DateTime.Today) < 0 && StartingDate.CompareTo(DateTime.Today) < 0)
            {
                validDates = true;
            }

            return validParameters && validDates;
        }

        private void ExecuteSearchSaleCommand(object obj)
        {
            IUserRepository userRepository = new UserRepository();
            ISaleRepository saleRepository = new SaleRepository();
            _salesOfSearch = saleRepository.GetByDateRange(StartingDate, EndingDate);
            ObservableCollection<SaleModel> sales_aux = new ObservableCollection<SaleModel>();
            foreach (SaleModel sale in _salesOfSearch)
            {
                if (sale.id_vendedor == SellerID)
                {
                    sales_aux.Add(sale);
                }
            }
            _salesOfSearch = sales_aux;
        }
    }
}
