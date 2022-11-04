using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroFamily.Model
{
    public interface ICashierRepository
    {
        void Add(CashierModel cashierModel);
        void Edit(CashierModel cashier);
        void Remove(int id);
        CashierModel GetById(int id);
        IEnumerable<CashierModel> GetByAll();
    }
}
