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
        public string salerName { get; set; }
        public DateTime SaleDate { get; set; }
        public long total { get; set; }
    }
}

