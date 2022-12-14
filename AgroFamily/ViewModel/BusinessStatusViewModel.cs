using AgroFamily.Model;
using AgroFamily.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Globalization;
using System.Security.Policy;

namespace AgroFamily.ViewModel
{
    public class BusinessStatusViewModel : ViewModelBase
    {
        private string _amountSales;
        private string _amountExp;
        private string _AmountDiff;
        private string _AmountDiffPorcent;


        public string AmountSales { get => _amountSales; set { _amountSales = value; OnPropertyChanged(nameof(AmountSales)); } }
        public string AmountExp { get => _amountExp; set { _amountExp = value; OnPropertyChanged(nameof(AmountExp)); } }
        public string AmountDiff { get => _AmountDiff; set { _AmountDiff = value; OnPropertyChanged(nameof(AmountDiff)); } }
        public string AmountDiffPorcent { get => _AmountDiffPorcent; set { _AmountDiffPorcent = value; OnPropertyChanged(nameof(AmountDiffPorcent)); } }



        public BusinessStatusViewModel()
        {
            DateTime dt = DateTime.Now;
            int month = dt.Month;
            int year = dt.Year;

            ISaleRepository saleRepository = new SaleRepository();
            IExpensiveModel expensiveRepository = new ExpensiveRepository();
            long AmountSales_int = saleRepository.GetAmountInAMonth(month, year);
            double AmountExp_int = expensiveRepository.GetAmountInAMonth(month, year);
            double AmountDiff_int = (AmountSales_int - AmountExp_int);
            double AmountDiffPorcent_int = (((AmountDiff_int)/AmountSales_int)*100);

            //string mstring = String.Format("{0:C}", AmountSales_int);

            //AmountSales = "$ " + Convert.ToString(AmountSales_int);
            AmountSales = String.Format("{0:C}", AmountSales_int); ;
            AmountExp = String.Format("{0:C}", AmountExp_int);
            AmountDiff = String.Format("{0:C}", AmountDiff_int);
            String PorcentFinal = String.Format("{0:N}", AmountDiffPorcent_int);
            AmountDiffPorcent = PorcentFinal + "%";





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
                TextSize = 80;
                TitleSize = 80;
                ButtonHeight1 = 80;
                ButtonWidth1 = 65;
            }



        }

        public string GetOwnFormat(string p_StrNumber)
        {
            string strResult = p_StrNumber.Replace(",", "#").Replace(".", ",").Replace("#", ".");

            return strResult;
        }



    }
}
