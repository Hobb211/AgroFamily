using AgroFamily.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroFamily.Repositories
{
    public class SaleProductRepository : RepositoryBase, ISaleProductRepository
    {
        public void Add(SaleProductModel sale)
        {
            using(SQLiteConnection connection = GetConnection())
            {
                connection.Insert(sale);
            }
        }

        public ObservableCollection<SaleProductModel> GetByAll()
        {
            IEnumerable<SaleProductModel> sales;
            using (SQLiteConnection connection = GetConnection())
            {
                sales = connection.Query<SaleProductModel>("select * from SaleProductModel");
            }

            ObservableCollection<SaleProductModel> collection = new ObservableCollection<SaleProductModel>(sales);
            return collection;
        }

        public ObservableCollection<SaleProductModel> GetBySale(string saleId)
        {
            IEnumerable<SaleProductModel> sales;
            using (SQLiteConnection connection = GetConnection())
            {
                sales = connection.Query<SaleProductModel>("select * from SaleProductModel where SaleId=" + saleId);
            }
            ObservableCollection<SaleProductModel> collection=new ObservableCollection<SaleProductModel>(sales);
            return collection;
        }
    }
}
