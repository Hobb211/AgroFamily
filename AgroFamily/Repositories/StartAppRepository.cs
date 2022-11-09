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
                db.CreateTable<ArticleModel>();
                try
                {
                    db.Insert(new UserModel() { Id = "Admin", Password = "Admin", Name = "Alpharius", Type = "Administrador" });
                    db.Insert(new TypeExpensiveModel() { Name = "Añadir tipo" });
                }
                catch { }
            }
        }
    }
}
