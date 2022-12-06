using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroFamily.Model
{
    public interface IExpensiveModel
    {
        public void Add(ExpensiveModel expensive);
        public ExpensiveModel GetById(int id);
        public ObservableCollection<ExpensiveModel> GetByAll();
        double GetAmountInAMonth(int month, int year);

    }
}
