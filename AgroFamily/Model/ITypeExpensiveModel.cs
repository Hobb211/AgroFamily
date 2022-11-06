using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroFamily.Model
{
    public interface ITypeExpensiveModel
    {
        public void Add(TypeExpensiveModel model);
        public ObservableCollection<TypeExpensiveModel> GetByAll();
    }
}
