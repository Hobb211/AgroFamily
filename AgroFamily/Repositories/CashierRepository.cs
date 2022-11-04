using AgroFamily.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroFamily.Repositories
{

    public class CashierRepository : RepositoryBase, ICashierRepository
    {
        public void Add(CashierModel cashierModel)
        {
            using (SQLiteConnection connection = GetConnection())
            {
                connection.CreateTable<CashierModel>();
                connection.Insert(cashierModel);
            }
        }

        public void Edit(CashierModel cashier)
        {
            using (SQLiteConnection connection = GetConnection())
            {
                connection.Update(cashier);
            }
        }

        public IEnumerable<CashierModel> GetByAll()
        {
            using (SQLiteConnection connection = GetConnection())
            {
                int cant = connection.Query<CashierModel>("select * from CashierModel").Count();
                CashierModel[] cashiers = new CashierModel[cant];
                for (int i = 1; i <= cant; i++)
                {
                    cashiers[i] = connection.Find<CashierModel>(i);
                }
                return cashiers;
            }
        }

        public CashierModel GetById(int id)
        {
            using (SQLiteConnection connection = GetConnection())
            {
                return connection.Find<CashierModel>(id);
            }
        }

        public void Remove(int id)
        {
            using (SQLiteConnection connection = GetConnection())
            {
                connection.Delete<CashierModel>(id);
            }
        }
    }

}
