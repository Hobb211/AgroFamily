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
    public class TypeUserRepository : RepositoryBase, ITypeUserRepository
    {

        public void Add(TypeUserModel typeUser)
        {
            using (SQLiteConnection connection = GetConnection())
            {
                connection.Insert(typeUser);
            }
        }

        public ObservableCollection<TypeUserModel> GetByAll()
        {
            TypeUserModel adminType = null;
            TypeUserModel cashierType = null;
            using (SQLiteConnection connection = GetConnection())
            {
                adminType = connection.Find<TypeUserModel>("Administrador");
                cashierType = connection.Find<TypeUserModel>("Cajero");
            }

            ObservableCollection<TypeUserModel> collection = new ObservableCollection<TypeUserModel>()
            {
                adminType,
                cashierType,
            };
            return collection;
        }

        public TypeUserModel GetById(string id)
        {
            try
            {
                using (SQLiteConnection connection = GetConnection())
                {
                    return connection.Find<TypeUserModel>(id);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
