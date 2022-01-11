using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ErtnaSoft.Bo.Entities;
using EtnaSoft.Bll.Models;
using EtnaSoft.Bo.Entities;

namespace EtnaSoft.Bll.Services
{
    public enum ReservationStatusType : int
    {
        CheckedIn = 1,
        Coming = 2
    }
    public interface ISchedulerService
    {
        ObservableCollection<Booking> LoadResource(object startDate = null, object endDate = null);
        List<StayType> LoadStayTypes();
        IEnumerable<CustomLabel> LoadCustomLabels();
    }
}