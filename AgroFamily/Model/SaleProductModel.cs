﻿using SQLite;
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
        public string Name { get; set; }
        public long Count { get; set; }
        public long Amount { get; set; }
    }
}
