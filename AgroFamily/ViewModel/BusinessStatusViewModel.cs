using AgroFamily.Model;
using AgroFamily.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AgroFamily.ViewModel
{
    public class BusinessStatusViewModel : ViewModelBase
    {
        private string _amountSales;
        private string _amountExp;
        private string _AmountDiff;

        public string AmountSales { get => _amountSales; set { _amountSales = value; OnPropertyChanged(nameof(AmountSales)); } }
        public string AmountExp { get => _amountExp; set { _amountExp = value; OnPropertyChanged(nameof(AmountExp)); } }
        public string AmountDiff { get => _AmountDiff; set { _AmountDiff = value; OnPropertyChanged(nameof(AmountDiff)); } }


        public BusinessStatusViewModel()
        {
            DateTime dt = DateTime.Now;
            int month = dt.Month;
            int year = dt.Year;

            ISaleRepository saleRepository = new SaleRepository();
            IExpensiveModel expensiveRepository = new ExpensiveRepository();
            int AmountSales_int = saleRepository.GetAmountInAMonth(month, year);
            int AmountExp_int = expensiveRepository.GetAmountInAMonth(month, year);
            int AmountDiff_int = (AmountSales_int - AmountExp_int);

            AmountSales = "$ " + Convert.ToString(AmountSales_int);
            AmountExp = "$ " + Convert.ToString(AmountExp_int);
            AmountDiff = "$ " + Convert.ToString(AmountDiff_int);

            TextSizeChange = 10;
            ButtonChangeSizeH = 20;
            ButtonChangeSizeW = 20;
            if ((bool)Application.Current.Properties["IsViewMinimize"])
            {
                TextSize = 15;
                TitleSize = 15;
                ButtonHeight1 = 20;
                ButtonWidth1 = 5;
            }
            else
            {
                TextSize = 35;
                TitleSize = 35;
                ButtonHeight1 = 60;
                ButtonWidth1 = 45;
            }

        }
    }
}
