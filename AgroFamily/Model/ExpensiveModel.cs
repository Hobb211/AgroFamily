using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroFamily.Model
{
    public class ExpensiveModel
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public long Amount { get; set; }
        public DateTime DateTime { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
    }
}
