using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ErtnaSoft.Bo.Entities;
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

        public string CheckIn(int id)
        {
            string success = "";
            Reservation resevation = _unit.Reservations.GetById(id);
            RoomReservation roomReservation = _unit.RoomReservations.GetById(resevation.RoomReservationId);
            var roomStatuses = _unit.RoomStatus.GetAll().FirstOrDefault(s => s.RoomReservationId == resevation.RoomReservationId);
            RoomStatus roomStatus = new RoomStatus();
            RoomStatus newStatus = new RoomStatus();
            if (roomStatuses != null)
            {
                roomStatus.DateUsed = resevation.StartDate;
                roomStatus.RoomId = roomReservation.RoomId;
                newStatus = _unit.RoomStatus.Create(roomStatus);

            }

            if (roomStatus.IsDirty)
            {
                return "Soba jos nije sredjena";
            }
            if (resevation != null)
            {
                resevation.IsCheckedIn = true;
                _unit.Reservations.Update(id, resevation);
                var updated = _unit.Reservations.GetById(id);
                var update = updated.IsCheckedIn;
                if (update)
                {
                    success = "Uspesno prijavljena soba!";
                }
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
