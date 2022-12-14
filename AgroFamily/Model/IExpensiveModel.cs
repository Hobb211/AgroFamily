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
        void Remove(int id);

        public ExpensiveModel GetById(int id);
        public ObservableCollection<ExpensiveModel> GetByAll();
        long GetAmountInAMonth(int month, int year);

        long GetAmountInARangeDate(string diaInicio, string mesInicio, string anoInicio, string diaFin, string mesFin, string anoFin);


    }
}
