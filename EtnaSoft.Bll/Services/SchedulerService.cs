using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Xml;
using ErtnaSoft.Bo.Entities;
using EtnaSoft.Bll.Models;
using EtnaSoft.Dal.Infrastucture;

namespace EtnaSoft.Bll.Services
{
    public class SchedulerService : ISchedulerService
    {
        private readonly IUnitOfWork _unit;
        public SchedulerService(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public int BookingsComingToday()
        {
            var reservation = _unit.Reservations.GetAll().Where(s => s.StartDate == DateTime.Now.Date && s.IsCheckedIn == false);
            return reservation.Count();
        }

        //TODO: Check loading resource;
        public ObservableCollection<Booking> LoadResource(object startDate = null, object endDate = null)
        {
            //TODO: Load by Request zavrsiti
            IEnumerable<Reservation> reservations = null;
            if (startDate != null && endDate != null)
            {
                var sDate = (DateTime)startDate;
                var eDate = (DateTime)endDate;
                startDate = null;
                endDate = null;
                reservations = _unit.Reservations.GetAll()
                    .Where(s=> (s.StartDate >= sDate || s.StartDate <= eDate && s.EndDate >= sDate || s.EndDate >= eDate) && s.IsCanceled == false);
            }
            else
            {
                //Default Load
                reservations = _unit.Reservations.GetAll()
                    .Where(s=> s.StartDate > DateTime.Now.Date.AddYears(-1) && s.IsCanceled == false);
            }

            int AssignLabelId(bool isCheckedIn)
            {
                int labelId = 0;
                labelId = (int) (isCheckedIn ? ReservationStatusType.CheckedIn : ReservationStatusType.Coming);


                return labelId;

            }
            //TODO: FULL BOOKING PROPERTIES
            
            ObservableCollection<Booking> bookings = new ObservableCollection<Booking>();
            
            foreach (var reservation in reservations)
            {
                var roomReservation = _unit.RoomReservations.GetAll().FirstOrDefault(s => s.Id == reservation.RoomReservationId);
                var guest = _unit.Guests.GetAll().FirstOrDefault(g=> g.Id == roomReservation.GuestId);
                var stayType = _unit.StayTypes.GetAll().FirstOrDefault(s=> s.Id == roomReservation.StayTypeId);
                var room = _unit.Rooms.GetAll().FirstOrDefault(r=> r.Id == roomReservation.RoomId);

                bookings.Add(new Booking()
                {
                    Id = reservation.Id,
                    AllDay = true, // All bookings are 24hours 
                    EndDate = reservation.EndDate,
                    StartDate = reservation.StartDate,
                    GuestId = guest.Id,
                    FirstName = guest.FirstName,
                    LastName = guest.LastName,
                    Telephone = guest.Telephone,
                    Address = guest.Address,
                    Email = guest.EmailAddress,
                    UniqueNumber = guest.UniqueNumber,
                    StayTypeId = stayType.Id,
                    Title = stayType.Title,
                    NumberOfPeople = reservation.NumberOfPeople,
                    RoomId = roomReservation.RoomId,
                    RoomNumber = room.RoomNumber,
                    IsCanceled = reservation.IsCanceled,
                    IsCheckedIn = reservation.IsCheckedIn,
                    CreatedBy = reservation.CreatedBy,
                    ModifiedBy = reservation.ModifiedBy,
                    DateCreated = reservation.DateCreated,
                    DateModified = reservation.DateModified,
                    InvoiceId = reservation.InvoiceId,
                    LabelId = AssignLabelId(reservation.IsCheckedIn)
                });
            }
            
            //var query = from r in reservations
            //    join rr in roomReservations on r.RoomReservationId equals rr.Id
            //    join room in rooms on rr.RoomId equals room.Id
            //    join g in guestList on rr.GuestId equals g.Id
            //    join s in stayType on rr.StayTypeId equals s.Id
            //    select new Booking()
            //    {
            //        Id = r.Id,
            //        AllDay = true, // All bookings are 24hours 
            //        EndDate = r.EndDate,
            //        StartDate = r.StartDate,
            //        GuestId = g.Id,
            //        FirstName = g.FirstName,
            //        LastName = g.LastName,
            //        Telephone = g.Telephone,
            //        Address = g.Address,
            //        Email = g.EmailAddress,
            //        UniqueNumber = g.UniqueNumber,
            //        StayTypeId = s.Id,
            //        Title = s.Title,
            //        NumberOfPeople = r.NumberOfPeople,
            //        RoomId = rr.RoomId,
            //        RoomNumber = room.RoomNumber,
            //        IsCanceled = r.IsCanceled,
            //        IsCheckedIn = r.IsCheckedIn,
            //        CreatedBy = r.CreatedBy,
            //        ModifiedBy = r.ModifiedBy,
            //        DateCreated = r.DateCreated,
            //        DateModified = r.DateModified,
            //        TotalPrice = r.TotalPrice,
            //        LabelId = AssignLabelId(r.IsCheckedIn)
            //    };

           
            return bookings;
        }

        public List<StayType> LoadStayTypes()
        {
            var output = _unit.StayTypes.GetAll().ToList();
            return output;
        }
        public IEnumerable<CustomLabel> LoadCustomLabels()
        {
            var output = _unit.Labels.GetAll();
            return output;
        }

     
    }
}
