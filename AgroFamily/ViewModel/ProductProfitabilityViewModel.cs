using AgroFamily.Model;
using AgroFamily.Repositories;
using System;
using Microsoft.VisualBasic.ApplicationServices;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;

namespace AgroFamily.ViewModel
{
    public class ProductProfitabilityViewModel : ViewModelBase
    {
        private ObservableCollection<ArticleModel> _articles;
        private ObservableCollection<SaleProductModel> _profits;
        private string _textInformation;

        public ObservableCollection<ArticleModel> Articles { get => _articles; set { _articles = value; OnPropertyChanged(nameof(Articles)); } }
        public ObservableCollection<SaleProductModel> Profits { get => _profits; set { _profits = value; OnPropertyChanged(nameof(Profits)); } }
        public string TextInformation { get => _textInformation; set { _textInformation = value; OnPropertyChanged(nameof(TextInformation)); } }
        //Commands
        public ICommand ShowTop10ASCCommand { get; }
        public ICommand ShowTop10DESCCommand { get; }

        public ICommand ShowAllCommand { get; }

        public ProductProfitabilityViewModel()
        {
            //IArticleRepository articleRepository = new ArticleRepository();
            //Articles = articleRepository.GetByAll();

            IProductRepository productRepository = new ProductRepository();
            ISuppliesRepository suppliesRepository = new SuppliesRepository();
            ISaleProductRepository saleProductRepository = new SaleProductRepository();

            Profits = new ObservableCollection<SaleProductModel>(saleProductRepository.getroductProfitabilityAll());
            ShowTop10ASCCommand = new ViewModelCommand(ExecuteShowTop10ASCCommand, CanExecuteShowTop10ASCCommand);
            ShowTop10DESCCommand = new ViewModelCommand(ExecuteShowTop10DESCCommand, CanExecuteShowTop10DESCCommand);
            ShowAllCommand = new ViewModelCommand(ExecuteShowAllCommand, CanExecuteShowAllCommand);
            TextInformation = "Mostrando lista de los productos mas rentables";

            TextSizeChange = 4;
            ButtonChangeSizeH = 20;
            ButtonChangeSizeW = 40;
            if ((bool)Application.Current.Properties["IsViewMinimize"])
            {
                TextSize = 9;
                TitleSize = 20;
                ButtonHeight1 = 20;
                ButtonWidth1 = 80;
            }
            else
            {
                TextSize = 21;
                TitleSize = 28;
                ButtonHeight1 = 80;
                ButtonWidth1 = 200;
            }
        }

        private bool CanExecuteShowTop10ASCCommand(object obj)
        {
            return true;
        }

        private void ExecuteShowTop10ASCCommand(object obj)
        {

            ISaleProductRepository saleProductRepository = new SaleProductRepository();
            Profits = new ObservableCollection<SaleProductModel>(saleProductRepository.getTop10ProductProfitabilityASC(10));

            TextInformation = "El producto menos rentable es " + saleProductRepository.getTop10ProductProfitabilityASC(1)[0].Name + " con una ganancia de $" + saleProductRepository.getTop10ProductProfitabilityASC(1)[0].Amount;

        }

        private bool CanExecuteShowTop10DESCCommand(object obj)
        {
            return true;
        }

        private void ExecuteShowTop10DESCCommand(object obj)
        {

            ISaleProductRepository saleProductRepository = new SaleProductRepository();
            Profits = new ObservableCollection<SaleProductModel>(saleProductRepository.getTop10ProductProfitabilityDESC(10));

            TextInformation = "El producto mas rentable es " + saleProductRepository.getTop10ProductProfitabilityDESC(1)[0].Name + " con una ganancia de $" + saleProductRepository.getTop10ProductProfitabilityDESC(1)[0].Amount;
        }
        private bool CanExecuteShowAllCommand(object obj)
        {
            return true;
        }

        private void ExecuteShowAllCommand(object obj)
        {

            ISaleProductRepository saleProductRepository = new SaleProductRepository();
            Profits = new ObservableCollection<SaleProductModel>(saleProductRepository.getroductProfitabilityAll());

            TextInformation = "Mostrando lista de los productos mas rentables";
        }




    }
}
