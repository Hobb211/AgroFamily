using AgroFamily.Model;
using Microsoft.VisualBasic.ApplicationServices;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Windows;
using System.Collections.ObjectModel;
using AgroFamily.Exceptions;

namespace AgroFamily.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public void Add(UserModel userModel)
        {
            using (SQLiteConnection connection = GetConnection())
            {
                connection.Insert(userModel);
            }
        }

        public bool AuthenticatePassword(NetworkCredential credential)
        {
            bool validPassword;
            using (SQLiteConnection connection = GetConnection())
            {
                UserModel user = connection.Find<UserModel>(credential.UserName);
                if (user.Password.Equals(credential.Password))
                {
                    validPassword = true;
                }
                else
                {
                    validPassword = false;
                }
            }
            return validPassword;
        }

        public bool AuthenticateUser(NetworkCredential credential)
        {
            bool validUser;
            using (SQLiteConnection connection = GetConnection())
            {
                UserModel user = connection.Find<UserModel>(credential.UserName);
                validUser = user != null;
            }
            return validUser;
        }

        public void Edit(UserModel user)
        {
            using (SQLiteConnection connection = GetConnection())
            {
                connection.Update(user);
            }
        }

        public ObservableCollection<UserModel> GetByAll()
        {
            IEnumerable<UserModel> users;
            using (SQLiteConnection connection = GetConnection())
            {
                users = connection.Query<UserModel>("select * from UserModel");
            }

            ObservableCollection<UserModel> collection = new ObservableCollection<UserModel>(users);
            return collection;
        }

        public UserModel GetById(string id)
        {
            UserModel user = null;
            using (SQLiteConnection connection = GetConnection())
            {
                user = connection.Find<UserModel>(id);
            }
            if (user == null)
            {
                throw new UserConflictException($"ID no valido {id}");
            }
            return user;

        }
        public string[] GetRole(NetworkCredential credential)
        {
            throw new NotImplementedException();
        }
        public void Remove(string id)
        {
            using (SQLiteConnection connection = GetConnection())
            {
                connection.Delete<UserModel>(id);
            }
        }
        public string GetSeller(UserModel user)
        {
            if (user != null)
            {
                return user.Name + ' ' + user.LastName;
            }
            else
            {
                throw new UserConflictException("No se ha dado un usuario valido");
            }
        }
    }
}
