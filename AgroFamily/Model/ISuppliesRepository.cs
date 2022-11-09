using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroFamily.Model
{
    public interface ISuppliesRepository
    {
        void Add(SuppliesModel supplies);
        void Edit(SuppliesModel supplies);
        void Remove(int id);
        SuppliesModel GetById(int id);
        IEnumerable<SuppliesModel> GetAll();
        ObservableCollection<SuppliesModel> GetByAll3();


    }
}
