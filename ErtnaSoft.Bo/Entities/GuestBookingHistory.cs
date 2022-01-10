using System;
using EtnaSoft.Bo.Entities;

namespace ErtnaSoft.Bo.Entities
{
    public class GuestBookingHistory 
    {
        private DateTime _startDate;

        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        private DateTime _endDate;

        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }

        private bool _isCheckedIn;

        public bool IsCheckedIn
        {
            get { return _isCheckedIn; }
            set { _isCheckedIn = value; }
        }

        private bool _isCanceled;

        public bool IsCanceled
        {
            get { return _isCanceled; }
            set { _isCanceled = value; }
        }

        private string _roomNumber;

        public string RoomNumber
        {
            get { return _roomNumber; }
            set { _roomNumber = value; }
        }


        //StayType Title
        private string _title;

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        private decimal _totalPrice;

        public decimal TotalPrice
        {
            get { return _totalPrice; }
            set { _totalPrice = value; }
        }
    }
}
