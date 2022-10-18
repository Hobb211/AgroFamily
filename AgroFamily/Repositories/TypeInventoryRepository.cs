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
    public class TypeInventoryRepository : RepositoryBase, ITypeInventoryRepository
    {
        public void Add(TypeInventoryModel typeInventory)
        {
            using (SQLiteConnection connection = GetConnection())
            {
                connection.CreateTable<TypeInventoryModel>();
                connection.Insert(typeInventory);
            }
        }

        public ObservableCollection<TypeInventoryModel> GetByAll()
        {
            TypeInventoryModel productType=null;
            TypeInventoryModel suppliesType=null;
            using (SQLiteConnection connection = GetConnection())
            {
                productType = connection.Find<TypeInventoryModel>("Producto");
                suppliesType = connection.Find<TypeInventoryModel>("Suministro");
            }

            ObservableCollection<TypeInventoryModel> collection = new ObservableCollection<TypeInventoryModel>()
            {
                productType,
                suppliesType,
            };
            return collection;
        }

        public TypeInventoryModel GetById(string id)
        {
            try
            {
                using (SQLiteConnection connection = GetConnection())
                {
                    return connection.Find<TypeInventoryModel>(id);
                }
            }
            catch (Exception ex)
            { 
                return null; 
            } 
        }
    }
}
