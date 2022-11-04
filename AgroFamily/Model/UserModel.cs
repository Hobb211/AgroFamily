using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace AgroFamily.Model
{

    public class UserModel
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Type { get; set; }
        public string Password { get; set; }

        //[PrimaryKey, AutoIncrement]
        //public int Id_auto { get; set; }

        //[Unique]
        //public string Id { get; set; }
        //public string Name { get; set; }
        //public string Lastname { get; set; }
        //public string Type { get; set; }
        //public string Password { get; set; }
    }
    //public class UserModel
    //{
    //    [PrimaryKey]
    //    public string Id { get; set; }
    //    public string Name { get; set; }
    //    public string LastName { get; set; }
    //    public string Type { get; set; }
    //    public string Password { get; set; }
    //}
}
