using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgroFamily.Model;
using SQLite;

namespace AgroFamily.Repositories
{
    public class SaleRepository : RepositoryBase, ISaleRepository
    {
        public void Add(SaleModel saleModel)
        {
            using (SQLiteConnection connection = GetConnection())
            {
                connection.Insert(saleModel);
            }
        }

        public ObservableCollection<SaleModel> GetAll()
        {
            IEnumerable<SaleModel> list;
            using (SQLiteConnection connection = GetConnection())
            {
                list = connection.Query<SaleModel>("select * from SaleModel");
            }
            return new ObservableCollection<SaleModel>(list);
        }

        public int GetAmountInAMonth(int month, int year) //A
        {
            int Amount = 0;
            IEnumerable<SaleModel> list;
            using (SQLiteConnection connection = GetConnection())
            {
                //list = connection.Query<SaleModel>("\tselect t1.Id, t1.id_vendedor, strftime('%m', t1.date) as mes, strftime('%Y', t1.date) as ano, t1.total\r\n\tfrom(\r\n\t\tselect Id, id_vendedor, datetime(datetime/10000000 - 62135596800, 'unixepoch') as date, total\r\n\t\tfrom SaleModel ) as t1\r\n\twhere mes =\"11\" and ano = \"\"\r\n");
                list = connection.Query<SaleModel>("select t1.Id, t1.id_vendedor, strftime('%m', t1.date) as mes, strftime('%Y', t1.date) as ano, t1.total from( select Id, id_vendedor, datetime(datetime/10000000 - 62135596800, 'unixepoch') as date, total from SaleModel ) as t1 where mes = \""+month+ "\" and ano = \"" + year+ "\"");
            }
            ObservableCollection<SaleModel> SaleModels = new ObservableCollection<SaleModel>(list);
            for (int i = 0; i < SaleModels.Count; i++)
            {
                SaleModel saleModel = SaleModels[i];
                Amount = Amount + saleModel.total;
            }

            return Amount;
        }

        public SaleModel GetById(int id)
        {
            using (SQLiteConnection connection = GetConnection())
            {
                return connection.Find<SaleModel>(id);
            }
        }

        public ObservableCollection<SaleModel> GetByDay(DateOnly date)
        {
            ObservableCollection<SaleModel> saleModels = GetAll();
            ObservableCollection<SaleModel> saleModelsDay = new ObservableCollection<SaleModel>();
            for (int i=0; i < saleModels.Count; i++)
            {
                if (DateOnly.FromDateTime(saleModels[i].SaleDate).CompareTo(date)==0)
                {
                    saleModelsDay.Add(saleModels[i]);
                }
            }
            return saleModelsDay;
        }


        //Este metodo lo cree para poder buscar las ventas en un rango dado, para poder satisfacer el historial de ventas 
        public ObservableCollection<SaleModel> GetByDateRange(DateOnly startingDate, DateOnly endingDate)
        {
            ObservableCollection<SaleModel> saleModels = GetAll();
            ObservableCollection<SaleModel> saleModelsOnRange = new ObservableCollection<SaleModel>();
            for (int i = 0; i < saleModels.Count; i++)
            {
                if (DateOnly.FromDateTime(saleModels[i].SaleDate).CompareTo(startingDate) == 0)
                {
                    saleModelsOnRange.Add(saleModels[i]);
                }
            }
            return saleModelsOnRange;
        }
        public ObservableCollection<SaleModel> GetBySeller(string sellerID)
        {
            ObservableCollection<SaleModel> saleModels = GetAll();
            foreach(SaleModel sale in saleModels)
            {
                if (sale.id_vendedor != sellerID)
                {
                    saleModels.Remove(sale);
                }
            }
            return saleModels;
        }
    }
}
