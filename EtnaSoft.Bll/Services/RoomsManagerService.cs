using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ErtnaSoft.Bo.Entities;
using EtnaSoft.Dal.Infrastucture;

namespace EtnaSoft.Bll.Services
{
    public sealed class RoomsManagerService : IRoomsManagerService
    {
        private readonly IUnitOfWork _unit;
        
        public RoomsManagerService(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public List<Room> GetAllRooms()
        {
            var rooms = _unit.Rooms.GetAll().ToList();
            return rooms;
        }
    }
}
