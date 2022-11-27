using AgroFamily.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Xml.Linq;

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

        public ObservableCollection<SaleProductModel> GetBySale(int saleId)
        {
            IEnumerable<SaleProductModel> sales;
            using (SQLiteConnection connection = GetConnection())
            {
                sales = connection.Query<SaleProductModel>("select * from SaleProductModel where SaleId=" + saleId);
            }
            ObservableCollection<SaleProductModel> collection=new ObservableCollection<SaleProductModel>(sales);
            return collection;
        }

        public ObservableCollection<SaleProductModel> getroductProfitabilityAll()
        {
            IEnumerable<SaleProductModel> sales;
            using (SQLiteConnection connection = GetConnection())
            {
                sales = connection.Query<SaleProductModel>("SELECT name,sum(count) as count ,sum(Amount) as Amount from SaleProductModel GROUP by name ORDER by sum(Amount) DESC");

            }
            ObservableCollection<SaleProductModel> collection = new ObservableCollection<SaleProductModel>(sales);
            return collection;
        }

        public ObservableCollection<SaleProductModel> getTop10ProductProfitabilityASC(int limite)
        {
            IEnumerable<SaleProductModel> sales;
            using (SQLiteConnection connection = GetConnection())
            {
                sales = connection.Query<SaleProductModel>("SELECT name,sum(count) as count ,sum(Amount) as Amount from SaleProductModel GROUP by name ORDER by sum(Amount) ASC limit " + limite);

            }
            ObservableCollection<SaleProductModel> collection = new ObservableCollection<SaleProductModel>(sales);
            return collection;
        }


        public ObservableCollection<SaleProductModel> getTop10ProductProfitabilityDESC(int limite)
        {
            IEnumerable<SaleProductModel> sales;
            using (SQLiteConnection connection = GetConnection())
            {
                sales = connection.Query<SaleProductModel>("SELECT name,sum(count) as count ,sum(Amount) as Amount from SaleProductModel GROUP by name ORDER by sum(Amount) DESC limit " + limite);

            }
            ObservableCollection<SaleProductModel> collection = new ObservableCollection<SaleProductModel>(sales);
            return collection;
        }

        public ObservableCollection<SaleProductModel> consultaName(string nameProduct,int consultaActual)
        {
            IEnumerable<SaleProductModel> sales;
            using (SQLiteConnection connection = GetConnection())
            {
                if (consultaActual == 0)
                {
                    sales = connection.Query<SaleProductModel>("WITH primero AS(SELECT name,sum(count) as count ,sum(Amount) as Amount from SaleProductModel GROUP by name ORDER by sum(Amount) DESC) SELECT * from primero where name like '%" + nameProduct +"%'");

                }
                else if (consultaActual == 1)
                {
                    sales = connection.Query<SaleProductModel>("WITH primero AS(SELECT name,sum(count) as count ,sum(Amount) as Amount from SaleProductModel GROUP by name ORDER by sum(Amount) ASC limit 10) SELECT * from primero where name like '%" + nameProduct + "%'");

                }
                else
                {
                    sales = connection.Query<SaleProductModel>("WITH primero AS(SELECT name,sum(count) as count ,sum(Amount) as Amount from SaleProductModel GROUP by name ORDER by sum(Amount) DESC limit 10) SELECT * from primero where name like '%" + nameProduct + "%'");

                }
            }
            ObservableCollection<SaleProductModel> collection = new ObservableCollection<SaleProductModel>(sales);
            return collection;
        }
    }
}
