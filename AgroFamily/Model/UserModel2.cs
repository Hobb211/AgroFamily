﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroFamily.Model
{
    public class UserModel2
    {
        [PrimaryKey, AutoIncrement]
        public int Id_auto { get; set; }

        [Unique]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Type { get; set; }
        public string Password { get; set; }
    }
}
