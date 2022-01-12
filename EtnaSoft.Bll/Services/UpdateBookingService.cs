using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ErtnaSoft.Bo.Entities;
using EtnaSoft.Dal.Infrastucture;
using EtnaSoft.Dal.Stores;

namespace EtnaSoft.Bll.Services
{

    public class UpdateBookingService : IUpdateBookingService
    {
        private readonly IUnitOfWork _unit;
        public UpdateBookingService(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public bool Update(Reservation reservation, Guest guest, string roomNumber, StayType stayType, bool isGuestDirty, bool isReservationDirty, bool isRoomReservationDirty)
        {
            ParameterNullCheck();
            bool success = false;
            var reservationFromDb = _unit.Reservations.GetById(reservation.Id);
            var roomReservation = _unit.RoomReservations.GetById(reservationFromDb.RoomReservationId);
            var room = _unit.Rooms.GetAll().FirstOrDefault(s => s.RoomNumber == roomNumber);
            var guestFromDb = _unit.Guests.GetById(roomReservation.GuestId);
            


            try
            {
               
                if (isRoomReservationDirty)
                {
                    success = UpdateRoomReservation();
                }

                if (isGuestDirty)
                {
                    success = UpdateGuest();
                }

                if (isReservationDirty)
                {
                    success = UpdateReservation();
                }
                
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

          


            return success;

            //LOCAL FUNCTIONS

            bool UpdateReservation()
            {
                bool updated = false;
                reservation.ModifiedBy = UserStore.CurrentUser;
                reservation.RoomReservationId = roomReservation.Id;
                updated =_unit.Reservations.Update(reservationFromDb.Id, reservation);
                return updated;
            }
            bool UpdateGuest()
            {
                bool updated = false;
                //IS DIRTY FLAG must be a conditional for modifiedby
                guestFromDb.ModifiedBy = UserStore.CurrentUser;
                updated = _unit.Guests.Update(guestFromDb.Id, guest);
                return updated;
            }
            void ParameterNullCheck()
            {
                if (reservation is null || guest is null || string.IsNullOrWhiteSpace(roomNumber) || stayType is null)
                {
                    throw new Exception("Parameter is null");
                }
            }
            bool UpdateRoomReservation()
            {
                bool updated = false;
                roomReservation.RoomId = room.Id;
                roomReservation.StayTypeId = stayType.Id;
                roomReservation.ModifiedBy = UserStore.CurrentUser;
                updated = _unit.RoomReservations.Update(roomReservation.Id, roomReservation);
                return updated;
            }
        }
    }
}
