using AgroFamily.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroFamily.Repositories
{
    public class AdminRepository : RepositoryBase, IAdminRepository
    {
        public void Add(AdminModel adminModel)
        {
            using (SQLiteConnection connection = GetConnection())
            {
                connection.CreateTable<AdminModel>();
                
                connection.Insert(adminModel);
            }
        }

        public void Edit(AdminModel admin)
        {
            using (SQLiteConnection connection = GetConnection())
            {
                connection.Update(admin);
            }
        }

        public IEnumerable<AdminModel> GetByAll()
        {
            using (SQLiteConnection connection = GetConnection())
            {
                int cant = connection.Query<AdminModel>("select * from AdminModel").Count();
                AdminModel[] admins = new AdminModel[cant];
                for (int i = 1; i <= cant; i++)
                {
                    admins[i] = connection.Find<AdminModel>(i);
                }
                return admins;
            }
        }

        public AdminModel GetById(int id)
        {
            using (SQLiteConnection connection = GetConnection())
            {
                return connection.Find<AdminModel>(id);
            }
        }

        public void Remove(int id)
        {
            using (SQLiteConnection connection = GetConnection())
            {
                connection.Delete<AdminModel>(id);
            }
        }
    }

}
