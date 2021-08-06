using System.Collections.ObjectModel;
using EtnaSoft.Bll.Models;

namespace EtnaSoft.Bll.Services
{
    public interface IBookingService
    {
        ObservableCollection<Booking> LoadResource();
    }
}