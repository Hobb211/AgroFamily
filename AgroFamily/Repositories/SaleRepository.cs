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
                if (DateOnly.FromDateTime(saleModels[i].dateTime).CompareTo(date)==0)
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
                if (DateOnly.FromDateTime(saleModels[i].dateTime).CompareTo(startingDate) == 0)
                {
                    saleModelsOnRange.Add(saleModels[i]);
                }
            }
            return saleModelsOnRange;
        }
    }
}
