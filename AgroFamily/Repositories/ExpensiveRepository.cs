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
    public class ExpensiveRepository : RepositoryBase, IExpensiveModel
    {
        public void Add(ExpensiveModel expensive)
        {
            using (SQLiteConnection connection = GetConnection())
            {
                connection.Insert(expensive);
            }
        }

        public ObservableCollection<ExpensiveModel> GetByAll()
        {
            using(SQLiteConnection connection = GetConnection())
            {
                IEnumerable<ExpensiveModel>expensives=connection.Query<ExpensiveModel>("select * from ExpensiveModel");
                return new ObservableCollection<ExpensiveModel>(expensives);
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
