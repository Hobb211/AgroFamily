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
    public class TypeExpensiveRepository : RepositoryBase, ITypeExpensiveModel
    {
        public void Add(TypeExpensiveModel model)
        {
            using(SQLiteConnection connection = GetConnection())
            {
                connection.CreateTable<TypeExpensiveModel>();
                connection.Insert(model);
            }
        }

        public ObservableCollection<TypeExpensiveModel> GetByAll()
        {
            using(SQLiteConnection connection= GetConnection())
            {
                int cant = connection.Query<TypeExpensiveModel>("select * from TypeExpensiveModel").Count();
                ObservableCollection<TypeExpensiveModel>types = new ObservableCollection<TypeExpensiveModel>();
                for (int i = 0; i < cant; i++)
                {
                    types.Add(connection.Find<TypeExpensiveModel>(i));
                }
                return types;
            }
        }
    }
}
