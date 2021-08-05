using System.Collections.Generic;
using System.Collections.ObjectModel;
using EtnaSoft.Bo.Entities;

namespace EtnaSoft.Bll.Services
{
    public interface IRoomResourceService
    {
        ObservableCollection<Room> CreateResource();
    }
}