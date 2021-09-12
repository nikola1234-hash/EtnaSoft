using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using EtnaSoft.Bo.Entities;
using EtnaSoft.Dal.Infrastucture;

namespace EtnaSoft.Dal.Services
{
    public class AvailableRoomsService : IAvailableRoomsService
    {
        private static string SearchForAvailableRooms = "sp_LoadFreeRooms";

        private readonly IGenericDbContext _dbContext;

        public AvailableRoomsService(IGenericDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ObservableCollection<Room> LoadAvailableRooms(DateTime startDate, DateTime endDate)
        {
            ObservableCollection<Room> result;
            object parameters = new
            {
                StartDate = startDate,
                EndDate = endDate
            };
            var availableRooms = _dbContext.LoadData<Room, dynamic>(SearchForAvailableRooms, parameters);
            result = new ObservableCollection<Room>(availableRooms);
            return result;
        }
    }
}
