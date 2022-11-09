using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroFamily.Model
{
    public interface IArticleRepository
    {
        void Add(ArticleModel articleModel);
        void Edit(ArticleModel article);
        void Remove(int id);
        ArticleModel GetById(int id);
        ObservableCollection<ArticleModel> GetByAll();
        ObservableCollection<ArticleModel> GetAllProducts();
    }

}
