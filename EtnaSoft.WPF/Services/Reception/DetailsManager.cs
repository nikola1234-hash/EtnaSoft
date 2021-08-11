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
            bool success = false;

            var reservation = new Reservation()
            {
                Id = (int) model.Appointment.Id,
                StartDate = model.StartDate.Date,
                EndDate = model.EndDate.Date,
                TotalPrice = model.TotalPrice
            };
            var roomNumber = model.RoomNumber;
            var stayType = model.SelectedStayType;
            Guest guest = new Guest()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = model.Address,
                BirthDate = model.BirthDate,
                EmailAddress = model.EmailAddress,
                ModifiedBy = UserStore.CurrentUser,
                UniqueNumber = model.UniqueNumber,
                Telephone = model.Telephone
            };
            

            
            _updateBooking.Update(reservation, guest, roomNumber, stayType);


            return success;
        }
    }
}
