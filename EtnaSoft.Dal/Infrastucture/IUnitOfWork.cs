﻿using ErtnaSoft.Bo.Entities;
using EtnaSoft.Bo.Entities;

namespace EtnaSoft.Dal.Infrastucture
{
    public interface IUnitOfWork
    {
        IRepository<Guest> Guests { get; }
        IRepository<User> Users { get; }
        IRepository<Room> Rooms { get; }
        IRepository<Reservation> Reservations { get; }
        IRepository<RoomReservation> RoomReservations { get; }
        IRepository<StayType> StayTypes { get; }
        IRepository<CustomLabel> Labels { get; }
    }
}