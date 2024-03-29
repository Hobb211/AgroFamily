﻿using AgroFamily.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroFamily.Model
{
    public interface ISaleProductRepository
    {
        public void Add(SaleProductModel sale);
        public ObservableCollection<SaleProductModel> GetBySale(int saleId);
        public ObservableCollection<SaleProductModel> GetByAll();

        public ObservableCollection<SaleProductModel> getTop10ProductProfitabilityASC(int limite);

        public ObservableCollection<SaleProductModel> getTop10ProductProfitabilityDESC(int limite);

        public ObservableCollection<SaleProductModel> getroductProfitabilityAll();

        public ObservableCollection<SaleProductModel> consultaName(string nameProduct,int consultaActual);

    }
}
