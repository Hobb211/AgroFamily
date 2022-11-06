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
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public string id_vendedor { get; set; }
        public string fecha_Venta { get; set; }

    }
}

