using System.Collections.Generic;
using System.Collections.ObjectModel;
using EtnaSoft.Bll.Models;
using EtnaSoft.Bo.Entities;

namespace EtnaSoft.Bll.Services
{
    public interface IBookingService
    {
        ObservableCollection<Booking> LoadResource();
        List<StayType> LoadStayTypes();
    }
}