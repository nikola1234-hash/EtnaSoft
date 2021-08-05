using System.Collections.Generic;
using System.Collections.ObjectModel;
using EtnaSoft.Bo.Entities;
using EtnaSoft.Dal.Infrastucture;

namespace EtnaSoft.Bll.Services
{
    public class RoomResourceService : IRoomResourceService
    {

        private readonly IUnitOfWork _unit;
        public RoomResourceService(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public ObservableCollection<Room> CreateResource()
        {
            var rooms = _unit.Rooms.GetAll();
            return new ObservableCollection<Room>(rooms);
        }
    }
}