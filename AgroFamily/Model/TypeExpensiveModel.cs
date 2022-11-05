using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroFamily.Model
{
    public class TypeExpensiveModel
    {
        [PrimaryKey]
        public string Name { get; set; }
    }
}
