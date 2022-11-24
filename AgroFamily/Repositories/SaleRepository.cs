using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AgroFamily.Exceptions;
using AgroFamily.Model;
using Microsoft.VisualBasic.ApplicationServices;
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
            IEnumerable<SaleModel> list = null;
            using (SQLiteConnection connection = GetConnection())
            {
                list = connection.Query<SaleModel>("select * from SaleModel");
            }
            if (list != null && list.Any())
            {
                return new ObservableCollection<SaleModel>(list);
            }
            else
            {
                throw new SaleConflictException("No se ha obtenido ninguna venta");
            }
        }

        public int GetAmountInAMonth(int month, int year) //A
        {
            int Amount = 0;
            IEnumerable<SaleModel> list;
            using (SQLiteConnection connection = GetConnection())
            {
                //list = connection.Query<SaleModel>("\tselect t1.Id, t1.id_vendedor, strftime('%m', t1.date) as mes, strftime('%Y', t1.date) as ano, t1.total\r\n\tfrom(\r\n\t\tselect Id, id_vendedor, datetime(datetime/10000000 - 62135596800, 'unixepoch') as date, total\r\n\t\tfrom SaleModel ) as t1\r\n\twhere mes =\"11\" and ano = \"\"\r\n");
                list = connection.Query<SaleModel>("select t1.Id, t1.id_vendedor, strftime('%m', t1.date) as mes, strftime('%Y', t1.date) as ano, t1.total from( select Id, id_vendedor, datetime(datetime/10000000 - 62135596800, 'unixepoch') as date, total from SaleModel ) as t1 where mes = \"" + month + "\" and ano = \"" + year + "\"");
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
            SaleModel saleModel = null;
            using (SQLiteConnection connection = GetConnection())
            {
                saleModel = connection.Find<SaleModel>(id);
            }
            if (saleModel == null)
            {
                throw new SaleConflictException($"No existe la venta {id}");
            }
            else
            {
                return saleModel;
            }
        }

        public ObservableCollection<SaleModel> GetByDay(DateOnly date)
        {
            ObservableCollection<SaleModel> saleModels = GetAll();
            ObservableCollection<SaleModel> saleModelsDay = new ObservableCollection<SaleModel>();
            for (int i = 0; i < saleModels.Count; i++)
            {
                if (DateOnly.FromDateTime(saleModels[i].SaleDate).CompareTo(date) == 0)
                {
                    saleModelsDay.Add(saleModels[i]);
                }
            }
            return saleModelsDay;
        }


        //Este metodo lo cree para poder buscar las ventas en un rango dado, para poder satisfacer el historial de ventas 
        public ObservableCollection<SaleModel> GetByDateRange(DateOnly startingDate, DateOnly endingDate)
        {
            ObservableCollection<SaleModel> saleModelsOnRange = new ObservableCollection<SaleModel>();
            ObservableCollection<SaleModel> saleModels = null;
            try
            {
                saleModels = GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (saleModels != null || saleModels.Any())
            {
                foreach (SaleModel sale in saleModels)
                {
                    if (DateOnly.FromDateTime(sale.SaleDate).CompareTo(startingDate) >= 0 && DateOnly.FromDateTime(sale.SaleDate).CompareTo(endingDate) <= 0)
                    {
                        saleModelsOnRange.Add(sale);
                    }
                }
            }
            if (!saleModelsOnRange.Any())
            {
                throw new SaleConflictException($"No se han encontrado ventas desde {startingDate} hasta {endingDate}");
            }
            return saleModelsOnRange;
        }
        public ObservableCollection<SaleModel> GetBySeller(string sellerID)
        {
            ObservableCollection<SaleModel> saleModels = null;
            ObservableCollection<SaleModel> salesOfSeller = new ObservableCollection<SaleModel>();
            try
            {
                saleModels = GetAll();
            }
            catch (SaleConflictException e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (saleModels != null)
            {
                foreach (SaleModel sale in saleModels)
                {
                    if (sale.id_vendedor == sellerID)
                    {
                        salesOfSeller.Add(sale);
                    }
                }
            }
            if (!salesOfSeller.Any())
            {
                throw new SaleConflictException($"No se ha obtenido ninguna venta para el vendedor {sellerID}");
            }
            return salesOfSeller;
        }

        public SaleModel GetBySellerIDSaleID(string sellerID, int saleID)
        {
            UserModel user = null;
            SaleModel saleOf = null;
            ObservableCollection<SaleModel> saleModels = null;

            try
            {
                saleModels = GetBySeller(sellerID);
                user = new UserRepository().GetById(sellerID);
            }
            catch (SaleConflictException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (UserConflictException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (!string.IsNullOrEmpty(user.Id) && saleModels.Any())
            {
                foreach (SaleModel sale in saleModels)
                {
                    if (sale.Id == saleID)
                    {
                        saleOf = sale;
                    }
                }
            }
            if (saleOf == null)
            {
                throw new SaleConflictException($"No se ha encontrado la venta {saleID} del vendedor {sellerID}");
            }
            return saleOf;
        }
    }
}
