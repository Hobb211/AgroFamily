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
        ////void Add(UserModel userModel);
        ////void Edit(UserModel user);
        ////void Remove(int id);
        ////UserModel GetById(int id);
        ////IEnumerable<UserModel> GetByAll();
        ////ObservableCollection<UserModel> GetByAll2();
        ////ObservableCollection<UserModel> GetByAll3();

        bool AuthenticateUser(NetworkCredential credential);
        bool AuthenticatePassword(NetworkCredential credential);
        string[] GetRole(NetworkCredential credential);
        void Add(UserModel userModel);
        void Edit(UserModel userModel);
        void Remove(String id);
        UserModel GetById(String id);
        IEnumerable<UserModel> GetByAll();


    }
    //public interface IUserRepository
    //{
    //    bool AuthenticateUser(NetworkCredential credential);
    //    bool AuthenticatePassword(NetworkCredential credential);
    //    string[] GetRole(NetworkCredential credential);
    //    void Add(UserModel userModel);
    //    void Edit(UserModel userModel);
    //    void Remove(String id);
    //    UserModel GetById(String id);
    //    IEnumerable<UserModel> GetByAll();

    //}
}
