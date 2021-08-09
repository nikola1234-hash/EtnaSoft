using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using EtnaSoft.Bll.Models;
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
        //TODO: Check loading resource;
        public ObservableCollection<Booking> LoadResource()
        {
            //TODO: FULL BOOKING PROPERTIES
            var reservations = _unit.Reservations.GetAll()
                .Where(s=> s.StartDate > DateTime.Now.Date.AddYears(-1));
            var roomReservation = _unit.RoomReservations.GetAll();
            var guests = _unit.Guests.GetAll();
            var stayType = _unit.StayTypes.GetAll();
            var rooms = _unit.Rooms.GetAll();

            var query = from r in reservations
                join rr in roomReservation on r.RoomReservationId equals rr.Id
                join room in rooms on rr.RoomId equals room.Id
                join g in guests on rr.GuestId equals g.Id
                join s in stayType on rr.StayTypeId equals s.Id
                select new Booking()
                {
                    AllDay = true, // All bookings are 24hours 
                    EndDate = r.EndDate,
                    StartDate = r.StartDate,
                    GuestId = g.Id,
                    FirstName = g.FirstName,
                    LastName = g.LastName,
                    Telephone = g.Telephone,
                    Address = g.Address,
                    Email = g.EmailAddress,
                    UniqueNumber = g.UniqueNumber,
                    StayTypeId = s.Id,
                    Title = s.Title,
                    NumberOfPeople = r.NumberOfPeople,
                    RoomId = rr.RoomId,
                    RoomNumber = room.RoomNumber,
                    IsCanceled = r.IsCanceled,
                    IsCheckedIn = r.IsCheckedIn,
                    CreatedBy = r.CreatedBy,
                    ModifiedBy = r.ModifiedBy,
                    DateCreated = r.DateCreated,
                    DateModified = r.DateModified,
                    TotalPrice = r.TotalPrice
                };
            ObservableCollection<Booking> bookings = new ObservableCollection<Booking>(query);
            return bookings;
        }

        public List<StayType> LoadStayTypes()
        {
            var output = _unit.StayTypes.GetAll().ToList();
            return output;
        }
    }
}
