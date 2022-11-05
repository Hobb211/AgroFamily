using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using AgroFamily.Model;
using AgroFamily.Repositories;

namespace AgroFamily.ViewModel
{
    public class SaleViewModel: ViewModelBase 
    {
        //Fields
        private UserAccountModel _userAccount;
        private IUserRepository userRepository;

        public UserAccountModel UserAccount{
            get => _userAccount;
            set
            {
                _userAccount = value;
                OnPropertyChanged(nameof(UserAccount));
            }
        }

        public SaleViewModel()
        {
            userRepository = new UserRepository();
            LoadCurrentUserData();
        }

        private void LoadCurrentUserData()
        {
            var user = userRepository.GetById(Thread.CurrentPrincipal.Identity.Name);//Pasar un UserName por el principal
            if (user != null)
            {
                UserAccount = new UserAccountModel()
                {
                    UserName = user.Id,
                    Name = user.Name,
                    LastName = user.LastName,
                    Role = user.Type
                };
            }
            else
            {
                MessageBox.Show("Usuario invalido");
                Application.Current.Shutdown();
            }
        }
    }
}


