﻿using AgroFamily.Model;
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
using System.Windows.Input;

namespace AgroFamily.ViewModel
{
    public class BusinessStatusViewModel : ViewModelBase
    {
        private string _amountSales ="";
        private string _amountExp="";
        private string _AmountDiff="";
        private string _AmountDiffPorcent="";

        private DateTime _startingDate = DateTime.Today;
        private DateTime _endingDate = DateTime.Today;

        public DateTime StartingDate { get => _startingDate; set { _startingDate = value; OnPropertyChanged(nameof(StartingDate)); } }
        public DateTime EndingDate { get => _endingDate; set { _endingDate = value; OnPropertyChanged(nameof(EndingDate)); } }
        public string AmountSales { get => _amountSales; set { _amountSales = value; OnPropertyChanged(nameof(AmountSales)); } }
        public string AmountExp { get => _amountExp; set { _amountExp = value; OnPropertyChanged(nameof(AmountExp)); } }
        public string AmountDiff { get => _AmountDiff; set { _AmountDiff = value; OnPropertyChanged(nameof(AmountDiff)); } }
        public string AmountDiffPorcent { get => _AmountDiffPorcent; set { _AmountDiffPorcent = value; OnPropertyChanged(nameof(AmountDiffPorcent)); } }

        public ICommand SearchRangeSaleCommand { get; }


        public BusinessStatusViewModel()
        {
            DateTime dt = DateTime.Now;
            int month = dt.Month;
            int year = dt.Year;

            SearchRangeSaleCommand = new ViewModelCommand(ExecuteSearchRangeSaleCommand, CanExecuteSearchRangeSaleCommand);

            ISaleRepository saleRepository = new SaleRepository();
            IExpensiveModel expensiveRepository = new ExpensiveRepository();

            if(AmountSales == "")
            {
                long AmountSales_int = saleRepository.GetAmountInAMonth(month, year);
                double AmountExp_int = expensiveRepository.GetAmountInAMonth(month, year);
                double AmountDiff_int = (AmountSales_int - AmountExp_int);
                double AmountDiffPorcent_int = (((AmountDiff_int) / AmountSales_int) * 100);

                string mstring = String.Format("{0:C}", AmountSales_int);

                AmountSales = "$ " + Convert.ToString(AmountSales_int);
                AmountSales = String.Format("{0:C}", AmountSales_int); ;
                AmountExp = String.Format("{0:C}", AmountExp_int);

                AmountDiff = String.Format("{0:C}", AmountDiff_int);
                String PorcentFinal = String.Format("{0:N}", AmountDiffPorcent_int);
                AmountDiffPorcent = PorcentFinal + "%";


            }




            TextSizeChange = 10;
            ButtonChangeSizeH = 20;
            ButtonChangeSizeW = 20;
            if ((bool)Application.Current.Properties["IsViewMinimize"])
            {
                TextSize = 5;
                TitleSize = 15;
                ButtonHeight1 = 20;
                ButtonWidth1 = 20;
            }
            else
            {
                TextSize = 35;
                TitleSize = 45;
                ButtonHeight1 = 80;
                ButtonWidth1 = 80;
            }



        }
        private bool CanExecuteSearchRangeSaleCommand(object obj)
        {
            if(StartingDate==DateTime.MinValue || EndingDate==DateTime.MinValue)
            {
                return false;

            }
            else
            {
                return true;
            }
        }

        private void ExecuteSearchRangeSaleCommand(object obj)
        {
            ISaleRepository salesRepository = new SaleRepository();
            IExpensiveModel expensiveRepository = new ExpensiveRepository(); ;
            try
            {
                double AmountSales_long = salesRepository.GetAmountInARangeDate(StartingDate,EndingDate.AddDays(1));
                double AmountExp_long = expensiveRepository.GetAmountInARangeDate(StartingDate,EndingDate.AddDays(1));

                double AmountDiff_int = (AmountSales_long - AmountExp_long);

                AmountSales = String.Format("{0:C}", AmountSales_long);
                AmountExp = String.Format("{0:C}", AmountExp_long);
                AmountDiff = String.Format("{0:C}", AmountDiff_int);


                double AmountDiffPorcent_int = (((AmountDiff_int) / AmountSales_long) * 100);

                string PorcentFinal = String.Format("{0:N}", AmountDiffPorcent_int);
                AmountDiffPorcent = PorcentFinal + "%";

                //MessageBox.Show(diaInicio.ToString()+ mesInicio.ToString()+anoInicio.ToString()+ diaFin.ToString()+ mesFin.ToString()+anoFin.ToString());
                //MessageBox.Show(amountexp);

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }


        }



        public string GetOwnFormat(string p_StrNumber)
        {
            string strResult = p_StrNumber.Replace(",", "#").Replace(".", ",").Replace("#", ".");

            return strResult;
        }



    }
}
