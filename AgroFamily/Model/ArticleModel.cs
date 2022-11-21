using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroFamily.Model
{
    public class ArticleModel
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int Stock { get; set; }
        public bool IsEditable { get; set; }

    }
}
