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
        private ArticleModel _articulo;

        public int Cantidad { get => _cantidad; set { _cantidad = value; OnPropertyChanged(nameof(Cantidad)); } }
        public int Id { get => _id; set { _id = value; OnPropertyChanged(nameof(Id)); } }

        // private ObservableCollection<ArticleModel> _products;
        private ObservableCollection<ArticleModel> _articles;

        public ObservableCollection<ArticleModel> Articles { get => _articles; set { _articles = value; OnPropertyChanged(nameof(Articles)); } }
        //public ObservableCollection<ArticleModel> Products { get => _products; set => _products = value; }
        public ICommand ReduceStockCommand { get; }
        public ICommand AddStockCommand { get; }
        public ArticleModel Articulo { get => _articulo; set => _articulo = value; }

        public EditStockViewModel()
        {
            //IArticleRepository articleRepository = new ArticleRepository();
            //Products = articleRepository.GetAllProducts();


            //AddStockCommand = ViewModelCommand(ExecuteAddStockCommand, CanExecuteAddStockCommand);


            IProductRepository productRepository = new ProductRepository();
            ISuppliesRepository suppliesRepository = new SuppliesRepository();

            Articles = suppliesRepository.GetByAllArticles();
            Articles = new ObservableCollection<ArticleModel>(suppliesRepository.GetByAllArticles().Concat(productRepository.GetByAllArticles()));
        }


        private bool CanExecuteAddStockCommand(object obj)
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
                //ArticleRepository articleRepository = new ArticleRepository();
                //ArticleModel article = articleRepository.GetById(Id);
                //article.Stock = article.Stock + Cantidad;
                //articleRepository.Edit(article);


                ArticleRepository articleRepository = new ArticleRepository();
                Articulo.Stock = Articulo.Stock + Cantidad;
                articleReposito

                switch(Articulo)
                    switch (Type.Name)
                {
                    case "Administrador":
                        UserModel admin = new UserModel();
                        admin.Id = Id;
                        admin.Name = Name;
                        admin.LastName = Lastname;
                        admin.Password = Password;
                        admin.Type = "Administrador";
                        userRepository.Add(admin);
                        break;

                    case "Cajero":
                        UserModel cashier = new UserModel();
                        cashier.Id = Id;
                        cashier.Name = Name;
                        cashier.LastName = Lastname;
                        cashier.Password = Password;
                        cashier.Type = "Cajero";
                        userRepository.Add(cashier);
                        break;
                        MessageBox.Show("Usuario registrado");
                }
                Users = userRepository.GetByAll();

                MessageBox.Show("Se ha editado el stock con exito");
            }
            catch
            {
                MessageBox.Show("Un error ha ocurrido");
            }

        }

    }
}


