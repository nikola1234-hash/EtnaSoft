using System.Collections.Generic;
using System.Collections.ObjectModel;
using ErtnaSoft.Bo.Entities;
using EtnaSoft.Bll.Models;
using EtnaSoft.Bo.Entities;

namespace EtnaSoft.Bll.Services
{
    public interface ISchedulerService
    {
        ObservableCollection<Booking> LoadResource();
        List<StayType> LoadStayTypes();
        IEnumerable<CustomLabel> LoadCustomLabels();
    }
}