using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroFamily.Model
{
    public interface IUser2Repository
    {
        void Add(UserModel2 userModel2);
        void Edit(UserModel2 user);
        void Remove(int id);
        UserModel2 GetById(int id);
        IEnumerable<UserModel2> GetByAll();
        ObservableCollection<UserModel2> GetByAll2();
        ObservableCollection<UserModel2> GetByAll3();



    }
}
