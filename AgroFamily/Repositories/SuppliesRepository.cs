using AgroFamily.Model;
using SQLite;
using System;
using System.Collections.Generic;
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

        public IEnumerable<SuppliesModel> GetAll()
        {
            using (SQLiteConnection connection = GetConnection())
            {
                int cant = connection.Query<SuppliesModel>("select * from SuppliesModel").Count();
                SuppliesModel[] supplies = new SuppliesModel[cant];
                for (int i = 1; i <= cant; i++)
                {
                    supplies[i] = connection.Find<SuppliesModel>(i);
                }
                return supplies;
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
    }
}
