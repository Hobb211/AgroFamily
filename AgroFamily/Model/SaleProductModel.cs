using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroFamily.Model
{
    public class SaleProductModel
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int SaleId { get; set; }
        public int Count { get; set; }
    }
}
