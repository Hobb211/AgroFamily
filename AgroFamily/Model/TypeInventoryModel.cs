using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroFamily.Model
{
    public class TypeInventoryModel
    {
        [PrimaryKey]
        public string Name { get; set; }
    }
}
