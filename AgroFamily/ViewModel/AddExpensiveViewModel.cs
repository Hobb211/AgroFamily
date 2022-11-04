﻿using AgroFamily.Model;
using AgroFamily.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;

namespace AgroFamily.ViewModel
{
    public class AddExpensiveViewModel:ViewModelBase
    {
        //Fields
        private TypeExpensiveModel _type;
        private ObservableCollection<TypeExpensiveModel> _typeExpensives;
        private float _amount;
        private string _description;
        private Visibility _newTypeVisibility;
        private string _newType;

        //Propierties
        public TypeExpensiveModel Type 
        { 
            get => _type;
            set 
            { 
                _type = value; 
                OnPropertyChanged(nameof(Type));
                if(Type.Name.Equals("Añadir Tipo"))
                {
                    NewTypeVisibility = Visibility.Visible;
                }
                else
                {
                    NewTypeVisibility = Visibility.Collapsed;
                }
            } 
        }
        public ObservableCollection<TypeExpensiveModel> TypeExpensives { get => _typeExpensives; set { _typeExpensives = value; OnPropertyChanged(nameof(TypeExpensives)); } }
        public float Amount { get => _amount; set { _amount = value; OnPropertyChanged(nameof(Amount)); } }
        public string Description { get => _description; set { _description = value; OnPropertyChanged(nameof(Description)); } }
        public Visibility NewTypeVisibility { get => _newTypeVisibility; set { _newTypeVisibility = value; OnPropertyChanged(nameof(NewTypeVisibility)); } }
        public string NewType { get => _newType; set { _newType = value; OnPropertyChanged(nameof(NewType)); } }

        //Commands
        public ICommand AddExpensiveCommand { get;}
        

        //Constructor
        public AddExpensiveViewModel()
        {
            Amount = 0;
            AddExpensiveCommand=new ViewModelCommand(ExecuteAddExpensiveCommand, CanExecuteAddExpensiveCommand);
            TypeExpensives = new TypeExpensiveRepository().GetByAll();
        }

        private bool CanExecuteAddExpensiveCommand(object obj)
        {
            bool validData;
            if (Amount<=0 || Type == null)
            {
                validData = false;
            }
            else
            {
                if (Type.Name.Equals("Añadir Tipo"))
                {
                    if (string.IsNullOrWhiteSpace(NewType) || NewType.Length < 3)
                    {
                        validData = false;
                    }
                    else
                    {
                        validData = true;
                    }
                }
                else { validData = true; }
            }
            return validData;
        }

        private void ExecuteAddExpensiveCommand(object obj)
        {
            string typeExpensive;
            if(Type.Name.Equals("Añadir Tipo"))
            {
                TypeExpensiveModel typeExpensiveModel = new TypeExpensiveModel() { Name=NewType };
                TypeExpensiveRepository typeExpensiveRepository = new TypeExpensiveRepository();
                typeExpensiveRepository.Add(typeExpensiveModel);
                typeExpensive = typeExpensiveModel.Name;
            }
            else
            {
                typeExpensive = Type.Name;
            }
            ExpensiveModel expensive = new ExpensiveModel();
            expensive.Amount = Amount;
            expensive.Type = typeExpensive;
            expensive.DateTime = DateTime.Now;
            expensive.Description = Description;
            ExpensiveRepository expensiveRepository = new ExpensiveRepository();
            try
            {
                expensiveRepository.Add(expensive);
                MessageBox.Show("Se ha registrado el gasto con exito");
            }
            catch
            {
                MessageBox.Show("Un error ha ocurrido");
            }
            
        }
        
        
    }
}
