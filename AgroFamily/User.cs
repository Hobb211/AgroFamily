﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroFamily
{
    internal class User
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Type { get; set; }
        public string Password  { get; set; }

        public override string ToString()
        {
            return $"{Name} - {LastName} - {Type} - {Password}";
        }

    }
}
