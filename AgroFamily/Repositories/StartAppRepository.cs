using AgroFamily.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroFamily.Repositories
{
    public class StartAppRepository:RepositoryBase
    {
        public void CreateTable()
        {
            using (var db = GetConnection())
            {
                db.CreateTable<ExpensiveModel>();
                db.CreateTable<ProductModel>();
                db.CreateTable<SuppliesModel>();
                db.CreateTable<TypeExpensiveModel>();
                db.CreateTable<TypeInventoryModel>();
                db.CreateTable<TypeUserModel>();
                db.CreateTable<UserModel>();
                db.CreateTable<SaleModel>();
                db.CreateTable<SaleProductModel>();
                try
                {
                    db.Insert(new TypeExpensiveModel() { Name = "Añadir tipo" });
                }
                catch { }
            }
        }
    }
}
