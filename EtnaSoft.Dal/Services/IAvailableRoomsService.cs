using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using EtnaSoft.Bo.Entities;

namespace EtnaSoft.Dal.Services
{
    public interface IAvailableRoomsService
    {
        ObservableCollection<Room> LoadAvailableRooms(DateTime startDate, DateTime endDate);
    }
}