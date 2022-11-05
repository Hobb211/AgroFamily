using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroFamily.Model
{
    public class SaleModel
    {
        public string id_vendedor { get; set; }
        public string nombre_producto { get; set; }

        public int[] id_producto { get; set; }

        public string fecha_Venta { get; set; }

        public int[] cantidad { get; set; }

        public int[] precio_producto { get; set; }
    }
}

