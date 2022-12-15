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

        public void Remove(int id)
        {
            using (SQLiteConnection connection = GetConnection())
            {
                connection.Delete<ExpensiveModel>(id);
            }
        }

        public ExpensiveModel GetById(int id)
        {
            using(SQLiteConnection con = GetConnection())
            {
                return con.Find<ExpensiveModel>(id);
            }
        }
        public long GetAmountInAMonth(int month, int year) //A
        {
            long Amount = 0;
            IEnumerable<ExpensiveModel> list;
            using (SQLiteConnection connection = GetConnection())
            {
                list = connection.Query<ExpensiveModel>("select t1.Id, t1.Amount, strftime('%m', t1.date) as mes, strftime('%Y', t1.date) as ano, t1.Type, t1.Description from( select Id, Amount, datetime(datetime/10000000 - 62135596800, 'unixepoch') as date, Type, Description from ExpensiveModel ) as t1 where mes =\""+month+"\" and ano = \""+year+"\"");
            }
            ObservableCollection<ExpensiveModel> expensiveModels = new ObservableCollection<ExpensiveModel>(list);
            for (int i = 0; i < expensiveModels.Count; i++)
            {
                ExpensiveModel expensive = expensiveModels[i];
                Amount = Amount + expensive.Amount;
            }

            return Amount;
        }

        public long GetAmountInARangeDate(DateTime inicio, DateTime fin) //A
        {
            long Amount = 0;
            IEnumerable<ExpensiveModel> list;
            using (SQLiteConnection connection = GetConnection())
            {
                list = connection.Query<ExpensiveModel>("select * from ExpensiveModel where DateTime between "+inicio.Ticks+" and "+fin.Ticks);
            }
            ObservableCollection<ExpensiveModel> expensiveModels = new ObservableCollection<ExpensiveModel>(list);
            for (int i = 0; i < expensiveModels.Count; i++)
            {
                ExpensiveModel exp = expensiveModels[i];
                Amount = Amount + exp.Amount;
            }

            return Amount;
        }


    }
}
