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
                connection.CreateTable<ArticleModel>();
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

        public IEnumerable<ArticleModel> GetByAll()
        {
            using (SQLiteConnection connection = GetConnection())
            {
                int cant = connection.Query<ArticleModel>("select * from ArticleModel").Count();
                //int cant = connection.Query<ProductModel>("select * from ProductModel where ID").Count();
                ArticleModel[] products = new ArticleModel[cant];
                for (int i = 1; i <= cant; i++)
                {
                    products[i] = connection.Find<ArticleModel>(i);
                }
                return products;
            }
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


        public ObservableCollection<ArticleModel> GetByAll3()
        {
            IEnumerable<ArticleModel> products;
            using (SQLiteConnection connection = GetConnection())
            {
                products = connection.Query<ArticleModel>("select * from ArticleModel");
            }
            ObservableCollection<ArticleModel> collection = new ObservableCollection<ArticleModel>(products);
            return collection;
        }
    }
}
