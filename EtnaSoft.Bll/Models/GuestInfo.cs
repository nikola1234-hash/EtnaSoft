using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using ErtnaSoft.Bo.Entities;

namespace EtnaSoft.Bll.Models
{
    public class GuestInfo
    {
        private Guest _guest { get; set; }
        public GuestInfo(Guest guest)
        {
            _guest = guest;
        }

   

        public int Id
        {
            get { return _guest.Id; }
        }
        [Category("Kontakt")] 
        public string FirstName
        {
            get => _guest.FirstName;
        }
        [Category("Kontakt")]
        public string LastName
        {
            get => _guest.LastName;
        }
        [Category("Kontakt")]
        public string Telephone
        {
            get => _guest.Telephone;
        }
        [Category("Kontakt")]
        public string EmailAddress
        {
            get => _guest.EmailAddress;
        }
        [Category("Info")]
        public string Address
        {
            get => _guest.Address;
        }
        [Category("Info")]
        public string UniqueNumber
        {
            get => _guest.UniqueNumber;
        }
        [Category("Info")]
        public DateTime? BirthDate
        {
            get => _guest.BirthDate;
        }

        [Category("Info")] private bool _isActive;
        public bool IsActive
        {
            get => _guest.IsActive;
            set => _isActive = value;
        }
    }
}
