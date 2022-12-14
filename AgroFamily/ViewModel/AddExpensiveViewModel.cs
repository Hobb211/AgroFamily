using AgroFamily.Model;
using AgroFamily.Repositories;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace AgroFamily.ViewModel
{
    public class AddExpensiveViewModel : ViewModelBase
    {
        //Fields
        private TypeExpensiveModel _type;
        private ExpensiveModel _expensive;
        private ObservableCollection<TypeExpensiveModel> _typeExpensives;
        private ObservableCollection<ExpensiveModel> _expensiveModels;
        private long _amount;
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
                if (Type!=null)
                {
                    if (Type.Name.Equals("Añadir tipo"))
                    {
                        NewTypeVisibility = Visibility.Visible;
                    }
                    else
                    {
                        NewTypeVisibility = Visibility.Collapsed;
                    }
                }
                 
            }
        }
        public ObservableCollection<TypeExpensiveModel> TypeExpensives { get => _typeExpensives; set { _typeExpensives = value; OnPropertyChanged(nameof(TypeExpensives)); } }
        public long Amount { get => _amount; set { _amount = value; OnPropertyChanged(nameof(Amount)); } }
        public string Description { get => _description; set { _description = value; OnPropertyChanged(nameof(Description)); } }
        public Visibility NewTypeVisibility { get => _newTypeVisibility; set { _newTypeVisibility = value; OnPropertyChanged(nameof(NewTypeVisibility)); } }
        public string NewType { get => _newType; set { _newType = value; OnPropertyChanged(nameof(NewType)); } }
        public ObservableCollection<ExpensiveModel> ExpensiveModels { get => _expensiveModels; set { _expensiveModels = value; OnPropertyChanged(nameof(ExpensiveModels)); } }
        public ExpensiveModel Expensive { get => _expensive; set { _expensive = value; OnPropertyChanged(nameof(Expensive)); } }

        //Commands
        public ICommand AddExpensiveCommand { get; }
        public ICommand RemoveExpensiveCommand { get; }


        //Constructor
        public AddExpensiveViewModel()
        {
            IExpensiveModel repository = new ExpensiveRepository();
            ExpensiveModels = repository.GetByAll();
            Amount = 0;
            AddExpensiveCommand = new ViewModelCommand(ExecuteAddExpensiveCommand, CanExecuteAddExpensiveCommand);
            RemoveExpensiveCommand = new ViewModelCommand(ExecuteRemoveExpensiveCommand, CanExecuteRemoveExpensiveCommand);

            TypeExpensives = new TypeExpensiveRepository().GetByAll();

            TextSizeChange = 10;
            ButtonChangeSizeH = 20;
            ButtonChangeSizeW = 20;
            TextBoxChangeSize = 70;
            if ((bool)Application.Current.Properties["IsViewMinimize"])
            {
                TextSize = 3;
                TitleSize = 10;
                ButtonHeight1 = 20;
                ButtonWidth1 = 140;
                ButtonHeight2 = 90;
                TextBoxHeight = 20;
            }
            else
            {
                TextSize = 33;
                TitleSize = 40;
                ButtonHeight1 = 80;
                ButtonWidth1 = 200;
                ButtonHeight2 = 150;
                TextBoxHeight = 230;
            }
        }

        private bool CanExecuteAddExpensiveCommand(object obj)
        {
            bool validData;
            if (Amount <= 0 || Type == null)
            {
                validData = false;
            }
            else
            {
                if (Type.Name.Equals("Añadir Tipo"))
                {
                    if (string.IsNullOrWhiteSpace(NewType) || NewType.Length < 3 )
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
            if (Type.Name.Equals("Añadir tipo"))
            {
                TypeExpensiveModel typeExpensiveModel = new TypeExpensiveModel() { Name = NewType };
                TypeExpensiveRepository typeExpensiveRepository = new TypeExpensiveRepository();
                try
                {
                    typeExpensiveRepository.Add(typeExpensiveModel);
                }
                catch { }
                typeExpensive = typeExpensiveModel.Name;
                TypeExpensives = typeExpensiveRepository.GetByAll();
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
                ExpensiveModels = expensiveRepository.GetByAll();
                MessageBox.Show("Se ha registrado el gasto con exito");
            }
            catch
            {
                MessageBox.Show("Un error ha ocurrido");
            }

        }

        private void ExecuteRemoveExpensiveCommand(object obj)
        {
            try
            {
                IExpensiveModel expensiveRepository = new ExpensiveRepository();
                expensiveRepository.Remove(Expensive.Id);
                ExpensiveModels = expensiveRepository.GetByAll();
                MessageBox.Show("Se ha eliminado el gasto");

            }
            catch (Exception e)
            {
                MessageBox.Show("No se ha podido eliminar, " + e.Message);
            }
        }

        private bool CanExecuteRemoveExpensiveCommand(object obj)
        {
            return true;
        }


    }
}
