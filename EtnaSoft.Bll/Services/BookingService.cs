using System;
using System.Collections.Generic;
using System.Text;
using EtnaSoft.Bo.Entities;
using EtnaSoft.Dal;
using EtnaSoft.Dal.Infrastucture;
using EtnaSoft.Dal.Services;
using EtnaSoft.Dal.Stores;

namespace EtnaSoft.Bll.Services
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unit;

        public BookingService(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public bool UndoCheckIn(int id)
        {
            bool success = false;
            var reservation = _unit.Reservations.GetById(id);
            reservation.IsCheckedIn = false;
            success = _unit.Reservations.Update(id, reservation);
            return success;
        }

        public bool CheckInStatus(int id)
        {
            bool success = false;
            var reservation = _unit.Reservations.GetById(id);
            return reservation.IsCheckedIn;
        }

        public bool CheckIn(int id)
        {
            bool success = false;
            Reservation resevation = _unit.Reservations.GetById(id);
            if (resevation != null)
            {
                resevation.IsCheckedIn = true;
                _unit.Reservations.Update(id, resevation);
                var updated = _unit.Reservations.GetById(id);
                success = updated.IsCheckedIn;
            }

            return success;

        }

        public bool Cancel(int id)
        {
            bool success = false;
            Reservation reservation = _unit.Reservations.GetById(id);
            
            if (reservation != null)
            {
                if (reservation.IsCanceled)
                {
                    throw new Exception("Reservation already canceled.");
                }
                reservation.ModifiedBy = UserStore.CurrentUser;
                reservation.IsCanceled = true;
                success = _unit.Reservations.Update(id, reservation);
                
            }

            return success;
        }
    }
}
