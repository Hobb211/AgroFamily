using AgroFamily.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroFamily.Repositories
{
    public class ArticleRepository : RepositoryBase, IArticleRepository
    {
        public void Add(ArticleModel articleModel)
        {
            using (SQLiteConnection connection = GetConnection())
            {
                connection.Insert(articleModel);
            }
        }

        public void Edit(ArticleModel article)
        {
            using (SQLiteConnection connection = GetConnection())
            {
                connection.Update(article);
            }
        }


        public ObservableCollection<ArticleModel> GetByAll()
        {
            IEnumerable<ArticleModel> articles;
            using (SQLiteConnection connection = GetConnection())
            {
                articles = connection.Query<ArticleModel>("select * from ArticleModel");
            }
            ObservableCollection<ArticleModel> collection = new ObservableCollection<ArticleModel>(articles);
            return collection;
        }

        public ArticleModel GetById(int id)
        {
            using (SQLiteConnection connection = GetConnection())
            {
                return connection.Find<ArticleModel>(id);
            }
        }

        public void Remove(int id)
        {
            using (SQLiteConnection connection = GetConnection())
            {
                connection.Delete<ArticleModel>(id);
            }
        }


        public ObservableCollection<ArticleModel> GetAllProducts()
        {
            IEnumerable<ArticleModel> products;
            using (SQLiteConnection connection = GetConnection())
            {
                products = connection.Query<ArticleModel>("select * from ArticleModel where Type=\"Producto\"");
            }
            ObservableCollection<ArticleModel> collection = new ObservableCollection<ArticleModel>(products);
            return collection;
        }
    }
}
