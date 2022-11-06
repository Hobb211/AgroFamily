using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//referencia al ensamblado de componentes
using System.ComponentModel;

namespace AgroFamily.ViewModel
{
    //Creamos una clase abstracta para que solo sea usada a traves de la herencia
    //Creamos e implementamos la interfaz notificar cambios de propiedad 
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
