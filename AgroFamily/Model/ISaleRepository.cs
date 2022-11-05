using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroFamily.Model
{
    internal interface ISaleRepository
    {
        void Add(SaleModel saletModel);
        void Edit(SaleModel saleetModel);
        SaleModel GetById(int id);
        IEnumerable<SaleModel> GetAll();

        void agregar(SaleModel saleetModel);
        void eliminar(SaleModel saletModel);
        void establecerCambios(SaleModel saletModel);   

        void mostrarInventario(SaleModel saletModel);

    }
}
