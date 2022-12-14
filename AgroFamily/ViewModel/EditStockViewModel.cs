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
using Microsoft.VisualBasic.ApplicationServices;

namespace AgroFamily.ViewModel
{
    public class EditStockViewModel : ViewModelBase
    {
        private int _cantidad;
        private int _id;
        private string _name;
        private ArticleModel _articulo;

        public int Cantidad { get => _cantidad; set { _cantidad = value; OnPropertyChanged(nameof(Cantidad)); } }
        public int Id { get => _id; set { _id = value; OnPropertyChanged(nameof(Id)); } }
        public string Name { get => _name; set { _name = value; OnPropertyChanged(nameof(Name)); } }

        private ObservableCollection<ArticleModel> _articles;

        public ObservableCollection<ArticleModel> Articles { get => _articles; set { _articles = value; OnPropertyChanged(nameof(Articles)); } }
        public ICommand ReduceStockCommand { get; }
        public ICommand AddStockCommand { get; }
        public ArticleModel Articulo { get => _articulo; set { _articulo = value; OnPropertyChanged(nameof(Articulo)); } }

        public EditStockViewModel()
        {
            //IArticleRepository articleRepository = new ArticleRepository();
            //Products = articleRepository.GetAllProducts();

            
            AddStockCommand = new ViewModelCommand(ExecuteAddStockCommand, CanExecuteChangeStockCommand);
            ReduceStockCommand = new ViewModelCommand(ExecuteReduceStockCommand, CanExecuteChangeStockCommand);

            IProductRepository productRepository = new ProductRepository();
            ISuppliesRepository suppliesRepository = new SuppliesRepository();

            Articles = suppliesRepository.GetByAllArticles();
            Articles = new ObservableCollection<ArticleModel>(suppliesRepository.GetByAllArticles().Concat(productRepository.GetByAllArticles()));

            TextSizeChange = 10;
            ButtonChangeSizeH = 20;
            ButtonChangeSizeW = 20;
            if ((bool)Application.Current.Properties["IsViewMinimize"])
            {
                TextSize = 5;
                TitleSize = 10;
                ButtonHeight1 = 10;
                ButtonWidth1 = 70;
            }
            else
            {
                TextSize = 35;
                TitleSize = 40;
                ButtonHeight1 = 70;
                ButtonWidth1 = 130;
            }
        }


        private bool CanExecuteChangeStockCommand(object obj)
        {
            bool validdata;

            if (Cantidad.ToString().Length == 0 || Id.ToString().Length == 0)
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
                ProductRepository productRepository = new ProductRepository();
                SuppliesRepository suppliesRepository = new SuppliesRepository();
                long cantidad_actual = Articulo.Stock;
                long cant = cantidad_actual + Cantidad;
                switch (Articulo.Type)
                {
                    case "Producto":

                        ProductModel pr = (ProductModel) Articulo;
                        pr.Stock = cant;
                        productRepository.Edit(pr);
                        break;
                    case "Suministro":
                        SuppliesModel sup = (SuppliesModel) Articulo; 
                        sup.Stock = cant;
                        suppliesRepository.Edit(sup);
                        break;
                }
                Articles = new ObservableCollection<ArticleModel>(suppliesRepository.GetByAllArticles().Concat(productRepository.GetByAllArticles()));
                MessageBox.Show("Se ha editado el stock con exito. Se han agregado " + Cantidad + " unidades");
                Id = 0;
                Cantidad = 0;
            }
            catch (Exception e)
            {
                MessageBox.Show("Un error ha ocurrido"+ e.Message);
            }
        }

        private void ExecuteReduceStockCommand(object obj)
        {
            try
            {
                ProductRepository productRepository = new ProductRepository();
                SuppliesRepository suppliesRepository = new SuppliesRepository();
                long cantidad_actual = Articulo.Stock;
                long cant = cantidad_actual - Cantidad;
                if (cant >= 0)
                {
                    switch (Articulo.Type)
                    {
                        case "Producto":

                            ProductModel pr = (ProductModel)Articulo;
                            pr.Stock = cant;
                            productRepository.Edit(pr);
                            break;
                        case "Suministro":
                            SuppliesModel sup = (SuppliesModel)Articulo;
                            sup.Stock = cant;
                            suppliesRepository.Edit(sup);
                            break;
                    }
                    Articles = new ObservableCollection<ArticleModel>(suppliesRepository.GetByAllArticles().Concat(productRepository.GetByAllArticles()));
                    MessageBox.Show("Se ha editado el stock con exito. Se han eliminado " + Cantidad+ " unidades");
                    Id = 0;
                    Cantidad = 0;
                }
                else
                {
                    MessageBox.Show("No se puede quitar más stock del que hay");
                }

            }
            catch
            {
                MessageBox.Show("Un error ha ocurrido");
            }
        }

        public void ExecuteGetCoincidences()

        {
            IProductRepository productRepository = new ProductRepository();
            ISuppliesRepository suppliesRepository = new SuppliesRepository();

            Articles = new ObservableCollection<ArticleModel>(suppliesRepository.GetSuppliesCoincidences(Name).Concat(productRepository.GetProductCoincidences(Name)));


        }
    }
}


