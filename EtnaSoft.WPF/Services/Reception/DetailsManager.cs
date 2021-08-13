using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErtnaSoft.Bo.Entities;
using EtnaSoft.Bll.Services;
using EtnaSoft.Bll.Stores;
using EtnaSoft.Bo.Entities;
using EtnaSoft.WPF.ViewModels;

namespace EtnaSoft.WPF.Services.Reception
{
    public class DetailsManager : IDetailsManager
    {
        private readonly IUpdateBookingService _updateBooking;
        public DetailsManager(IUpdateBookingService updateBooking)
        {
            _updateBooking = updateBooking;
        }

        public bool CreateUpdateModel(AppointmentViewModel model)
        {
            Guest MapGuest(AppointmentViewModel appointmentViewModel)
            {
                Guest mappedGuest = new Guest()
                {
                    FirstName = appointmentViewModel.FirstName,
                    LastName = appointmentViewModel.LastName,
                    Address = appointmentViewModel.Address,
                    BirthDate = appointmentViewModel.BirthDate,
                    EmailAddress = appointmentViewModel.EmailAddress,
                    ModifiedBy = UserStore.CurrentUser,
                    UniqueNumber = appointmentViewModel.UniqueNumber,
                    Telephone = appointmentViewModel.Telephone
                };
                return mappedGuest;
            }
            Reservation MapReservation(AppointmentViewModel appointmentViewModel)
            {
                var mappedReservation = new Reservation()
                {
                    Id = (int) appointmentViewModel.Appointment.Id,
                    StartDate = appointmentViewModel.StartDate.Date,
                    EndDate = appointmentViewModel.EndDate.Date,
                    TotalPrice = appointmentViewModel.TotalPrice,
                    NumberOfPeople = appointmentViewModel.NumberOfPeople
                };
                return mappedReservation;
            }
            bool success = false;

            var roomNumber = model.RoomNumber;
            var stayType = model.SelectedStayType;
            var guest = MapGuest(model);
            var reservation = MapReservation(model);


            success = _updateBooking.Update(reservation, guest, roomNumber, stayType, model.IsGuestDirty,
                model.IsReservationDirty, model.IsRoomReservationDirty);


            return success;
        }
    }
}
