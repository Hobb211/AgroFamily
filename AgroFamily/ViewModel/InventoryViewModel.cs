﻿using AgroFamily.Model;
using AgroFamily.Repositories;
using System;
using Microsoft.VisualBasic.ApplicationServices;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroFamily.ViewModel
{
    public class InventoryViewModel : ViewModelBase
    {
        private ObservableCollection<ArticleModel> _articles;
        public ObservableCollection<ArticleModel> Articles { get => _articles; set { _articles = value; OnPropertyChanged(nameof(Articles)); } }
        public InventoryViewModel()
        {
            //IArticleRepository articleRepository = new ArticleRepository();
            //Articles = articleRepository.GetByAll();
        }
    }
}
