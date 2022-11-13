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

namespace AgroFamily.ViewModel
{
    public class InventoryViewModel : ViewModelBase
    {
        private ObservableCollection<ArticleModel> _articles;
        private ObservableCollection<ProductModel> _products;
        private ObservableCollection<SuppliesModel> _supplies;
        public ObservableCollection<ArticleModel> Articles { get => _articles; set { _articles = value; OnPropertyChanged(nameof(Articles)); } }
        public ObservableCollection<ProductModel> Products { get => _products; set { _products = value; OnPropertyChanged(nameof(Products)); } }
        public ObservableCollection<SuppliesModel> Supplies { get => _supplies; set { _supplies = value; OnPropertyChanged(nameof(Supplies)); } }
        public InventoryViewModel()
        {
            //IArticleRepository articleRepository = new ArticleRepository();
            //Articles = articleRepository.GetByAll();

            IProductRepository productRepository = new ProductRepository();
            ISuppliesRepository suppliesRepository = new SuppliesRepository();  

            Articles = suppliesRepository.GetByAllArticles();
            Articles = new ObservableCollection<ArticleModel>(suppliesRepository.GetByAllArticles().Concat(productRepository.GetByAllArticles()));

            
        }


    }
}
