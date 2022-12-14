using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroFamily.Model
{
    public class ProductModel:ArticleModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public long Price { get; set; }
        //public int Stock { get; set; }
    }
}
