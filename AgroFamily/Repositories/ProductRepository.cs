using AgroFamily.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace AgroFamily.Repositories
{
    public class ProductRepository : RepositoryBase, IProductRepository
    {
        public void Add(ProductModel productModel)
        {
            using (SQLiteConnection connection = GetConnection())
            {
                connection.Insert(productModel);
            }
        }

        public void Edit(ProductModel product)
        {
            using (SQLiteConnection connection = GetConnection())
            {
                connection.Update(product);
            }
        }

        public ObservableCollection<ProductModel> GetByAll()
        {
            IEnumerable<ProductModel> products;
            using (SQLiteConnection connection = GetConnection())
            {
                products = connection.Query<ProductModel>("select * from ProductModel");
            }

            ObservableCollection<ProductModel> collection = new ObservableCollection<ProductModel>(products);
            return collection;
        }

        public ProductModel GetById(int id)
        {
            using (SQLiteConnection connection = GetConnection())
            {
                return connection.Find<ProductModel>(id);
            }
        }

        public void Remove(int id)
        {
            using (SQLiteConnection connection= GetConnection())
            {
                connection.Delete<ProductModel>(id);
            }
        }

        public ObservableCollection<ArticleModel> GetByAllArticles()
        {
            ObservableCollection<ArticleModel> collection;
            IEnumerable<ArticleModel> articles=GetByAll().AsEnumerable();
            collection = new ObservableCollection<ArticleModel>(articles);
            return collection;
        }


        //public ObservableCollection<ArticleModel> GetProductCoincidences()
        //{
        //    ObservableCollection<ArticleModel> collection;
        //    IEnumerable<ArticleModel> articles = GetByAll().AsEnumerable();
        //    collection = new ObservableCollection<ArticleModel>(articles);
        //    return collection;
        //}

        public ObservableCollection<ArticleModel> GetProductCoincidences(string coincidencia)
        {
            IEnumerable<ProductModel> products;
            using (SQLiteConnection connection = GetConnection())
            {
                products = connection.Query<ProductModel>("SELECT * FROM ProductModel where Name like '%" + coincidencia + "%'");
            }
            IEnumerable<ArticleModel> articles = products.AsEnumerable();

            ObservableCollection<ArticleModel> collection = new ObservableCollection<ArticleModel>(articles);
            return collection;
        }

        public ObservableCollection<ProductModel> GetProductCoincidences2(string coincidencia)
        {
            IEnumerable<ProductModel> products;
            using (SQLiteConnection connection = GetConnection())
            {
                products = connection.Query<ProductModel>("SELECT * FROM ProductModel where Name like '%" + coincidencia + "%'");
            }
            ObservableCollection<ProductModel> collection = new ObservableCollection<ProductModel>(products);

            return collection;
        }
    }
}
