using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ErtnaSoft.Bo.Entities;
using EtnaSoft.Dal.Exceptions;
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

        public Room CreateNewRoom(Room entity)
        {
            var newRoom = _unit.Rooms.Create(entity);
            return newRoom;
        }

        public void DeleteRoom(int id)
        {
            var areThereRecords = _unit.RoomReservations.GetAll().Where(s => s.RoomId == id);
            var room = _unit.Rooms.GetById(id);
            if (areThereRecords.Any())
            {
                throw new RoomHasRecordsException("Ne mozete obrisati sobu koja ima zapise poslovanja.", room);
            }

            _unit.Rooms.Delete(id);
        }

        public void UpdateRoom(int id, Room entity)
        {
            var room = _unit.Rooms.GetById(id);
            var e = _unit.Rooms.GetAll().Where(s => s.RoomNumber == entity.RoomNumber);
            if (e.Any(s=>s.RoomNumber == entity.RoomNumber))
            {
                throw new Exception("Vec postoji ova soba u bazi");
            }
            else
            {
                try
                {
                    _unit.Rooms.Update(id, entity);
                }
                catch
                {
                    throw;
                }

            }
        }
    }
}
