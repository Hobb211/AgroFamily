using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroFamily.Model
{
    public interface IProductReposiroty
    {
        void Add(ProductModel productModel);
        void Edit(ProductModel product);
        void Remove(int id);
        ProductModel GetById(int id);
        IEnumerable<ProductModel> GetByAll();
    }
}
