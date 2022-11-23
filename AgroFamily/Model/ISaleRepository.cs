using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroFamily.Model
{
    public interface ISaleRepository
    {
        void Add(SaleModel saletModel);
        SaleModel GetById(int id);
        ObservableCollection<SaleModel> GetAll();
        int GetAmountInAMonth(int month, int year);
        ObservableCollection<SaleModel> GetByDay(DateOnly date);
        ObservableCollection<SaleModel> GetByDateRange(DateOnly startingDate, DateOnly endingDate);
        ObservableCollection<SaleModel> GetBySeller(string sellerID);
    }
}
