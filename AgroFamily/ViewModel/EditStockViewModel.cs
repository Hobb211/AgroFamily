using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;
using AgroFamily.Model;
using AgroFamily.Repositories;

namespace AgroFamily.ViewModel
{
    public class EditStockViewModel : ViewModelBase
    {
        private int _cantidad;
        private int _id;
        private ArticleModel _articulo;

        public int Cantidad { get => _cantidad; set { _cantidad = value; OnPropertyChanged(nameof(Cantidad)); } }
        public int Id { get => _id; set { _id = value; OnPropertyChanged(nameof(Id)); } }

        private ObservableCollection<ArticleModel> _products;
        public ObservableCollection<ArticleModel> Products { get => _products; set => _products = value; }
        public ICommand ReduceStockCommand { get; }
        public ICommand AddStockCommand { get; }
        public ArticleModel Articulo { get => _articulo; set => _articulo = value; }

        public EditStockViewModel()
        {
            //IArticleRepository articleRepository = new ArticleRepository();
            //Products = articleRepository.GetAllProducts();


            //AddStockCommand = ViewModelCommand(ExecuteAddStockCommand, CanExecuteAddStockCommand);
        }


        private bool CanExecuteAddStockCommand(object obj)
        {
            bool validdata;

            if (Cantidad.ToString().Length == 0 || Id.ToString().Length==0)
            {
                validdata = false;
            }
            else
            {
                validdata = true;
            }
            return validdata;
        }

        private void ExecuteAddStockCommand(object obj)
        {
            try
            {
                //IArticleRepository articleRepository = new ArticleRepository();
                //ArticleModel article = articleRepository.GetById(Id);
                //article.Stock = article.Stock + Cantidad;
                //articleRepository.Edit(article);

                MessageBox.Show("Se ha editado el stock con exito");
            }
            catch
            {
                MessageBox.Show("Un error ha ocurrido");
            }

        }

    }
}


