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

namespace AgroFamily.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public void Add(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        public bool AuthenticatePassword(NetworkCredential credential)
        {
            bool validPassword;
            using (SQLiteConnection connection = GetConnection())
            {
                UserModel user = connection.Find<UserModel>(credential.UserName);
                if (user.Password.Equals(credential.Password)){
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

        public void Edit(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserModel> GetByAll()
        {
            throw new NotImplementedException();
        }

        public UserModel GetById(string id)
        {
            UserModel user=null;
            using (SQLiteConnection connection = GetConnection())
            {
                user = connection.Find<UserModel>(id);
            }
            return user;
        }

        public string[] GetRole(NetworkCredential credential)
        {
            throw new NotImplementedException();
        }

        public void Remove(string id)
        {
            throw new NotImplementedException();
        }
    }
}
