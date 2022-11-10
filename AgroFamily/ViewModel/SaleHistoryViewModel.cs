using AgroFamily.Model;
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
        private DateTime _startingDate;
        private DateTime _endingDate;
        private bool _onlyToday;
        private bool _dateRange;
        public readonly ObservableCollection<SaleModel> _salesOfSearch;

        //Properties
        public string SellerID { get => _sellerID; set { _sellerID = value; OnPropertyChanged(nameof(SellerID)); } }
        public string SaleID { get => _saleID; set { _saleID = value; OnPropertyChanged(nameof(SaleID)); } }
        public DateTime StartingDate { get => _startingDate; set { _startingDate = value; OnPropertyChanged(nameof(StartingDate)); } }
        public DateTime EndingDate { get => _endingDate; set { _endingDate = value; OnPropertyChanged(nameof(EndingDate)); } }
        public bool OnlyToday { get => _onlyToday; set { _onlyToday = value; OnPropertyChanged(nameof(OnlyToday)); } }
        public bool DateRange { get => _dateRange; set { _dateRange = value; OnPropertyChanged(nameof(DateRange)); } }

        //Commands
        public ICommand SearchSaleCommand { get; }

        public SaleHistoryViewModel()
        {
            _salesOfSearch = new ObservableCollection<SaleModel>();


            //Initialize Command
            SearchSaleCommand = new ViewModelCommand(ExecuteSearchSaleCommand, CanExecuteSearchSaleCommand);
        }

        private void ExecuteSearchSaleCommand(object obj)
        {
            return;
        }
        private bool CanExecuteSearchSaleCommand(object obj)
        {
            return true;
        }
    }
}
