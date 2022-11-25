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
    public class InventoryViewModel : ViewModelBase
    {
        private ObservableCollection<ArticleModel> _articles;
        private ObservableCollection<ProductModel> _products;
        private ObservableCollection<SuppliesModel> _supplies;
        private string _id;
        private ArticleModel _currentArticle;
        public string Id { get => _id; set { _id = value; OnPropertyChanged(nameof(Id)); } }
        public ArticleModel CurrentArticle { get => _currentArticle; set { _currentArticle = value; OnPropertyChanged(nameof(CurrentArticle)); } }


        public ObservableCollection<ArticleModel> Articles { get => _articles; set { _articles = value; OnPropertyChanged(nameof(Articles)); } }
        public ObservableCollection<ProductModel> Products { get => _products; set { _products = value; OnPropertyChanged(nameof(Products)); } }
        public ObservableCollection<SuppliesModel> Supplies { get => _supplies; set { _supplies = value; OnPropertyChanged(nameof(Supplies)); } }

        public ICommand RemoveItemCommand { get; }
        public InventoryViewModel()
        {
            //IArticleRepository articleRepository = new ArticleRepository();
            //Articles = articleRepository.GetByAll();

            IProductRepository productRepository = new ProductRepository();
            ISuppliesRepository suppliesRepository = new SuppliesRepository();  

            Articles = suppliesRepository.GetByAllArticles();
            Articles = new ObservableCollection<ArticleModel>(suppliesRepository.GetByAllArticles().Concat(productRepository.GetByAllArticles()));
            RemoveItemCommand = new ViewModelCommand(ExecuteRemoveArticleCommand, CanExecuteRemoveArticleCommand);

            TextSizeChange = 10;
            ButtonChangeSizeH = 20;
            ButtonChangeSizeW = 20;
            if ((bool)Application.Current.Properties["IsViewMinimize"])
            {
                TextSize = 3;
                ButtonHeight1 = 20;
                ButtonWidth1 = 140;
            }
            else
            {
                TextSize = 33;
                ButtonHeight1 = 80;
                ButtonWidth1 = 200;
            }
        }


        private void ExecuteRemoveArticleCommand(object obj)
        {
            try
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("¿Estás seguro de eliminar el artículo?", "Confirmación de eliminación", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    IProductRepository productRepository = new ProductRepository();
                    ISuppliesRepository suppliesRepository = new SuppliesRepository();

                    if (CurrentArticle.Type == "Producto")
                    {
                        ProductModel p = (ProductModel)CurrentArticle;
                        productRepository.Remove(p.Id);
                    }
                    else
                    {
                        SuppliesModel s = (SuppliesModel) CurrentArticle;
                        suppliesRepository.Remove(s.Id);
                    }
                    Articles = new ObservableCollection<ArticleModel>(suppliesRepository.GetByAllArticles().Concat(productRepository.GetByAllArticles()));
                    MessageBox.Show("Artículo eliminado");

                }
            }
            catch (Exception e)
            {
                MessageBox.Show("No se ha podido eliminar, " + e.Message);
            }
        }
        private bool CanExecuteRemoveArticleCommand(object obj) => CurrentArticle == null ? false : true;
        //{
        //    bool validData;
        //    try
        //    {
        //        if (CurrentUser == null)
        //        {
        //            validData = false;
        //        }
        //        else
        //        {
        //            validData = true;
        //        }
        //        return validData;

        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show(e.Message);
        //        return false;
        //    }

        //}
    }
}
