﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroFamily.Model
{
    public interface IProductRepository
    {
        void Add(ProductModel productModel);
        void Edit(ProductModel product);
        void Remove(int id);
        ProductModel GetById(int id);
        IEnumerable<ProductModel> GetByAll();

        ObservableCollection<ProductModel> GetByAll3();

    }
}
