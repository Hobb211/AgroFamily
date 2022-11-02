using AgroFamily.Model;
using Microsoft.VisualBasic;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
                if (cant == 0)
                {
                    MessageBox.Show("No hay tuplas");
                }
                AdminModel[] admins = new AdminModel[cant];
                for (int i = 0; i < cant; i++)
                {
                    admins[i] = connection.Find<AdminModel>(i+1);
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



        public ObservableCollection<AdminModel> GetByAll2()
        {
            AdminModel adminType = null;
            AdminModel adminType2 = null;
            using (SQLiteConnection connection = GetConnection())
            {
                adminType = connection.Find<AdminModel>("21");
                adminType2 = connection.Find<AdminModel>("21");
            }

            ObservableCollection<AdminModel> collection = new ObservableCollection<AdminModel>()
            {
                adminType,
                adminType2
            };

            return collection;
        }

        public ObservableCollection<AdminModel> GetByAll3()
        {
            IEnumerable<AdminModel> nose;
            AdminModel adminType = null;
            using (SQLiteConnection connection = GetConnection())
            {
                int cant = connection.Query<AdminModel>("select * from AdminModel").Count();
                nose = connection.Query<AdminModel>("select * from AdminModel");


            }

            ObservableCollection<AdminModel> collection = new ObservableCollection<AdminModel>(nose);
            return collection;
        }

        //public ObservableCollection<AdminModel> GetByAll3()
        //{
        //    using (SQLiteConnection connection = GetConnection())
        //    {
        //        SQLiteCommand mycommand = new SQLiteCommand(connection);
        //        mycommand.CommandText = "SELECT * from AdminModel";
        //        sqlite
        //    }
        //}


    }

}
