using AgroFamily.Repositories;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroFamily.Model
{

    public interface ITypeUserRepository
    {
        void Add(TypeUserModel typeUser);
        TypeUserModel GetById(string id);
        ObservableCollection<TypeUserModel> GetByAll();
    }
}
