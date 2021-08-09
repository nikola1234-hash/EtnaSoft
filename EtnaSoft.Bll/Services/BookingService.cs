using System;
using System.Collections.Generic;
using System.Text;
using EtnaSoft.Bo.Entities;
using EtnaSoft.Dal.Infrastucture;

namespace EtnaSoft.Bll.Services
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unit;

        public BookingService(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public void CheckIn(int id)
        {
            Reservation resevation = _unit.Reservations.GetById(id);
            if (resevation != null)
            {
                resevation.IsCheckedIn = true;
                _unit.Reservations.Update(id, resevation);
            }
        }

        public void Cancel(int id)
        {
            Reservation reservation = _unit.Reservations.GetById(id);
            
            if (reservation != null)
            {
                if (reservation.IsCanceled)
                {
                    throw new Exception("Reservation already canceled.");
                }

                reservation.IsCanceled = true;
                _unit.Reservations.Update(id, reservation);
            }
        }
    }
}
