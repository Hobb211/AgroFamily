using AgroFamily.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroFamily.Repositories
{
    public class ProductRepository : RepositoryBase, IProductRepository
    {
        public void Add(ProductModel productModel)
        {
            using (SQLiteConnection connection = GetConnection())
            {
                connection.CreateTable<ProductModel>();
                connection.Insert(productModel);
            }
        }

        public void Edit(ProductModel product)
        {
            using (SQLiteConnection connection = GetConnection())
            {
                connection.Update(product);
            }
        }

        public IEnumerable<ProductModel> GetByAll()
        {
            using (SQLiteConnection connection = GetConnection())
            {
                int cant = connection.Query<ProductModel>("select * from ProductModel").Count();
                ProductModel[] products = new ProductModel[cant];
                for (int i = 1; i <= cant; i++)
                {
                    products[i] = connection.Find<ProductModel>(i);
                }
                return products;
            }
        }

        public ProductModel GetById(int id)
        {
            using (SQLiteConnection connection = GetConnection())
            {
                return connection.Find<ProductModel>(id);
            }
        }

        public void Remove(int id)
        {
            using (SQLiteConnection connection= GetConnection())
            {
                connection.Delete<ProductModel>(id);
            }
        }
    }
}
