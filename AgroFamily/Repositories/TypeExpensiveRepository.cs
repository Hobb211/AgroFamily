using AgroFamily.Model;
using Microsoft.VisualBasic.ApplicationServices;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AgroFamily.Repositories
{
    public class TypeExpensiveRepository : RepositoryBase, ITypeExpensiveModel
    {
        public void Add(TypeExpensiveModel model)
        {
            using(SQLiteConnection connection = GetConnection())
            {
                connection.Insert(model);
            }
        }

        public ObservableCollection<TypeExpensiveModel> GetByAll()
        {
            IEnumerable<TypeExpensiveModel> types;
            using (SQLiteConnection connection= GetConnection())
            {
                types = connection.Query<TypeExpensiveModel>("select * from TypeExpensiveModel");  
            }
            ObservableCollection<TypeExpensiveModel> collection = new ObservableCollection<TypeExpensiveModel>(types);
            return collection;
        }
        
    }
}
