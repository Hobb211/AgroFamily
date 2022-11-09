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
    public class SuppliesRepository : RepositoryBase, ISuppliesRepository
    {
        public void Add(SuppliesModel supplies)
        {
            using (SQLiteConnection connection = GetConnection())
            {
                connection.Insert(supplies);
            }
        }

        public void Edit(SuppliesModel supplies)
        {
            using (SQLiteConnection connection = GetConnection())
            {
                connection.Update(supplies);
            }
        }

        public SuppliesModel GetById(int id)
        {
            using (SQLiteConnection connection = GetConnection())
            {
                return connection.Find<SuppliesModel>(id);
            }
        }

        public void Remove(int id)
        {
            using (SQLiteConnection connection = GetConnection())
            {
                connection.Delete<SuppliesModel>(id);
            }
        }

        public ObservableCollection<SuppliesModel> GetByAll()
        {
            IEnumerable<SuppliesModel> supplies;
            using (SQLiteConnection connection = GetConnection())
            {
                supplies = connection.Query<SuppliesModel>("select * from SuppliesModel");
            }

            ObservableCollection<SuppliesModel> collection = new ObservableCollection<SuppliesModel>(supplies);
            return collection;
        }

        public ObservableCollection<ArticleModel> GetByAllArticles()
        {
            ObservableCollection<ArticleModel> collection;
            IEnumerable<ArticleModel> articles = GetByAll().AsEnumerable();
            collection = new ObservableCollection<ArticleModel>(articles);
            return collection;
        }
    }



}
