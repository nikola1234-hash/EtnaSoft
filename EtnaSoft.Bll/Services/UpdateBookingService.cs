using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ErtnaSoft.Bo.Entities;
using EtnaSoft.Bll.Stores;
using EtnaSoft.Bo.Entities;
using EtnaSoft.Dal.Infrastucture;

namespace EtnaSoft.Bll.Services
{

    public class UpdateBookingService : IUpdateBookingService
    {
        private readonly IUnitOfWork _unit;
        public UpdateBookingService(IUnitOfWork unit)
        {
            _unit = unit;
        }

        //TODO DateTime overflow fix Reservation
        public bool Update(Reservation reservation, Guest guest, string roomNumber, StayType stayType)
        {
            if (reservation is null || guest is null || string.IsNullOrWhiteSpace(roomNumber) || stayType is null)
            {
                throw new Exception("Parameter is null");
            }
            bool success = false;
            var reservationFromDb = _unit.Reservations.GetById(reservation.Id);
            var roomReservation = _unit.RoomReservations.GetById(reservationFromDb.RoomReservationId);
            var room = _unit.Rooms.GetAll().FirstOrDefault(s => s.RoomNumber == roomNumber);
            var guestFromDb = _unit.Guests.GetById(roomReservation.GuestId);
            


            try
            {
                //RoomReservation Update first
                roomReservation.RoomId = room.Id;
                roomReservation.StayTypeId = stayType.Id;
                roomReservation.ModifiedBy = UserStore.CurrentUser;
                _unit.RoomReservations.Update(roomReservation.Id, roomReservation);
                
                //Guest UPDATE
                guestFromDb.ModifiedBy = UserStore.CurrentUser;
                _unit.Guests.Update(guestFromDb.Id, guest);
                
                //AND FINALY RESERVATION UPDATE
                reservation.ModifiedBy = UserStore.CurrentUser;
                _unit.Reservations.Update(reservationFromDb.Id, reservation);

                success = true;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return success;

        }
    }
}
