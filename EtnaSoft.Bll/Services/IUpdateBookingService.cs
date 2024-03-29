﻿using ErtnaSoft.Bo.Entities;

namespace EtnaSoft.Bll.Services
{
    public interface IUpdateBookingService
    {
        bool Update(Reservation reservation, Guest guest, string roomNumber, StayType stayType, bool isGuestDirty,
            bool isReservationDirty, bool isRoomReservationDirty);
    }
}