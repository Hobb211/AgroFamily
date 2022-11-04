using AgroFamily.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroFamily.Repositories
{
    public class ExpensiveRepository : RepositoryBase, IExpensiveModel
    {
        public void Add(ExpensiveModel expensive)
        {
            using (SQLiteConnection connection = GetConnection())
            {
                connection.CreateTable<ExpensiveModel>();
                connection.Insert(expensive);
            }
        }

        public IEnumerable<ExpensiveModel> GetByAll()
        {
            using(SQLiteConnection connection = GetConnection())
            {
                connection.CreateTable<ExpensiveModel>();
                int cant = connection.Query<ExpensiveModel>("select * from ExpensiveModel").Count();
                ExpensiveModel[] expensives = new ExpensiveModel[cant];
                for (int i = 1; i <= cant; i++)
                {
                    expensives[i] = connection.Find<ExpensiveModel>(i);
                }
                return expensives;
            }
        }

        public ExpensiveModel GetById(int id)
        {
            using(SQLiteConnection con = GetConnection())
            {
                return con.Find<ExpensiveModel>(id);
            }
        }
    }
}
