using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroFamily.Model
{
    public interface ITypeInventoryRepository
    {
        void Add(TypeInventoryModel typeInventory);
        TypeInventoryModel GetById(string id);
        ObservableCollection<TypeInventoryModel> GetByAll();
    }
}
