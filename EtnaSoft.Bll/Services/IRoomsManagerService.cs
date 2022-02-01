using System.Collections.Generic;
using ErtnaSoft.Bo.Entities;

namespace EtnaSoft.Bll.Services
{
    public interface IRoomsManagerService
    {
        List<Room> GetAllRooms();
        Room CreateNewRoom(Room entity);
        void DeleteRoom(int id);
        void UpdateRoom(int id, Room entity);
    }
}