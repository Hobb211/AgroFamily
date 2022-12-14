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
                    if (db.Query<UserModel>("select * from UserModel").Count() == 0) 
                    {
                        db.Insert(new UserModel() { Id = "admin",Password="admin",Type="Administrador"});
                    }
                    db.Insert(new TypeExpensiveModel() { Name = "Añadir tipo" });
                    db.Insert(new TypeUserModel() { Name="Administrador"});
                    db.Insert(new TypeUserModel() { Name = "Cajero" });
                }
                catch { }
            }
        }
    }
}
