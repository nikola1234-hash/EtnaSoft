using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErtnaSoft.Bo.Entities;

namespace EtnaSoft.WPF.ViewModels
{
    public class UserDetailsViewModel : EtnaBaseViewModel
    {
        public User User { get; set; }

        public UserDetailsViewModel(User user)
        {
            User = user;
        }
    }
}
