﻿using AgroFamily.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AgroFamily.View
{
    /// <summary>
    /// Lógica de interacción para SalesHistoryView.xaml
    /// </summary>
    public partial class SalesHistoryView : UserControl
    {
        public SalesHistoryView()
        {
            InitializeComponent();
        }
        private void Border_SizeChanged1(object sender, SizeChangedEventArgs e)
        {
            ViewModelBase viewModel = (ViewModelBase)this.DataContext;
            viewModel.ChangeSizeFont();
        }
    }
}