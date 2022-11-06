using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgroFamily.Model;
using SQLite;

namespace AgroFamily.Repositories
{
    public class SaleRepository : RepositoryBase, ISaleRepository
    {
        public void Add(SaleModel saleModel)
        {
            using (SQLiteConnection connection = GetConnection())
            {
                connection.Insert(saleModel);
            }
        }

        public void agregar(SaleModel saleetModel)
        {
            throw new NotImplementedException();
        }

        public void Edit(SaleModel saleModel)
        {
            using (SQLiteConnection connection = GetConnection())
            {
                connection.Update(saleModel);
            }
        }

        public void eliminar(SaleModel saletModel)
        {
            throw new NotImplementedException();
        }

        public void establecerCambios(SaleModel saletModel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SaleModel> GetAll()
        {
            using (SQLiteConnection connection = GetConnection())
            {
                int cant = connection.Query<SaleModel>("select * from SaleModel").Count();
                SaleModel[] saleModel = new SaleModel[cant];
                for (int i = 1; i <= cant; i++)
                {
                    saleModel[i] = connection.Find<SaleModel>(i);
                }
                return saleModel;
            }
        }

        public SaleModel GetById(int id)
        {
            using (SQLiteConnection connection = GetConnection())
            {
                return connection.Find<SaleModel>(id);
            }
        }

        public void mostrarInventario(SaleModel saletModel)
        {
            throw new NotImplementedException();
        }
    }
}
