using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AgroFamily.Model
{


    public interface IUserRepository
    {
        bool AuthenticateUser(NetworkCredential credential);
        bool AuthenticatePassword(NetworkCredential credential);
        string[] GetRole(NetworkCredential credential);
        void Add(UserModel userModel);
        void Edit(UserModel userModel);
        void Remove(String id);
        UserModel GetById(String id);
        ObservableCollection<UserModel> GetByAll();
        string GetSeller(UserModel user);

    }

}
