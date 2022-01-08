using System;

namespace EtnaSoft.WPF.Models
{
    public sealed class DataGridGuest : ObservableObject
    {
        #region Properties
        private string _firstName;

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (value == _firstName)
                    return;
                _firstName = value;
                OnPropertyChanged();
            }
        }

        private string _lastName;

        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (value == _lastName)
                    return;
                _lastName = value;
                OnPropertyChanged();
            }
        }

        private string _telephone;

        public string Telephone
        {
            get { return _telephone; }
            set
            {
                if (value == _telephone)
                    return;
                _telephone = value;
                OnPropertyChanged();
            }
        }

        private string _address;

        public string Address
        {
            get { return _address; }
            set
            {
                if (value == _address)
                    return;
                _address = value;
                OnPropertyChanged();
            }
        }

        private string _emailAddress;

        public string EmailAddress
        {
            get { return _emailAddress; }
            set
            {
                if (value == _emailAddress)
                    return;
                _emailAddress = value;
                OnPropertyChanged();
            }
        }

        private string _uniqueNumber;

        public string UniqueNumber
        {
            get { return _uniqueNumber; }
            set
            {
                if (value == _uniqueNumber)
                    return;
                _uniqueNumber = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _birthDate;

        public DateTime? BirthDate
        {
            get { return _birthDate; }
            set
            {
                if (value == _birthDate)
                    return;
                _birthDate = value;
                OnPropertyChanged();
            }
        }

        private int _id;

        public int Id
        {
            get { return _id; }
            set
            {
                if (value != _id)
                {
                    return;
                }
                _id = value;
                OnPropertyChanged();
            }
        }

        private string _createdBy;

        public string CreatedBy
        {
            get { return _createdBy; }
            set
            {
                if (value != _createdBy)
                {
                    return;
                }
                _createdBy = value;
                OnPropertyChanged();
            }
        }

        private string _modifiedBy;

        public string ModifiedBy
        {
            get { return _modifiedBy; }
            set
            {
                if (value == _modifiedBy)
                {
                    return;
                }
                _modifiedBy = value;
                OnPropertyChanged();
            }
        }

        private bool _isActive;

        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                if (value == _isActive)
                {
                    return;
                }
                _isActive = value;
                OnPropertyChanged();
            }
        }

        private DateTime _dateCreated;

        public DateTime DateCreated
        {
            get { return _dateCreated; }
            set
            {
                if (value != _dateCreated)
                {
                    return;
                }
                _dateCreated = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _dateModified;

        public DateTime? DateModified
        {
            get { return _dateModified; }
            set
            {
                if (value == _dateModified)
                {
                    return;
                }
                _dateModified = value;
                OnPropertyChanged();
            }

        }
        #endregion
    }
}
