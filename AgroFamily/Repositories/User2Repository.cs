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
    public class User2Repository : RepositoryBase, IUser2Repository
    {
        public void Add(UserModel2 userModel2)
        {
            using (SQLiteConnection connection = GetConnection())
            {
                connection.CreateTable<UserModel2>();

                connection.Insert(userModel2);
            }
        }

        public void Edit(UserModel2 user)
        {
            using (SQLiteConnection connection = GetConnection())
            {
                connection.Update(user);
            }
        }

        public IEnumerable<UserModel2> GetByAll()
        {
            using (SQLiteConnection connection = GetConnection())
            {
                int cant = connection.Query<UserModel2>("select * from UserModel2").Count();
                if (cant == 0)
                {
                    MessageBox.Show("No hay tuplas");
                }
                UserModel2[] users = new UserModel2[cant];
                for (int i = 0; i < cant; i++)
                {
                    users[i] = connection.Find<UserModel2>(i + 1);
                }
                return users;
            }
        }


        public UserModel2 GetById(int id)
        {
            using (SQLiteConnection connection = GetConnection())
            {
                return connection.Find<UserModel2>(id);
            }
        }

        public void Remove(int id)
        {
            using (SQLiteConnection connection = GetConnection())
            {
                connection.Delete<UserModel2>(id);
            }
        }



        public ObservableCollection<UserModel2> GetByAll2()
        {
            UserModel2 user1 = null;
            UserModel2 user2 = null;
            using (SQLiteConnection connection = GetConnection())
            {
                user1 = connection.Find<UserModel2>("21");
                user2 = connection.Find<UserModel2>("21");
            }

            ObservableCollection<UserModel2> collection = new ObservableCollection<UserModel2>()
            {
                user1,
                user2
            };

            return collection;
        }

        public ObservableCollection<UserModel2> GetByAll3()
        {
            IEnumerable<UserModel2> users;
            using (SQLiteConnection connection = GetConnection())
            {
                int cant = connection.Query<UserModel2>("select * from UserModel2").Count();
                users = connection.Query<UserModel2>("select * from UserModel2");



            }

            ObservableCollection<UserModel2> collection = new ObservableCollection<UserModel2>(users);
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
