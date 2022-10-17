using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroFamily.Model
{
    public interface IAdminRepository
    {
        void Add(AdminModel adminModel);
        void Edit(AdminModel admin);
        void Remove(int id);
        AdminModel GetById(int id);
        IEnumerable<AdminModel> GetByAll();
    }

}
