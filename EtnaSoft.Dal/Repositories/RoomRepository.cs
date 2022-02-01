using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ErtnaSoft.Bo.Entities;
using EtnaSoft.Dal.Infrastucture;

namespace EtnaSoft.Dal.Repositories
{
    public class RoomRepository : IRepository<Room>
    {
        private const string RoomsLoadAll = "Select * from dbo.Rooms";
        private const string InsertNewRoom = "Insert into dbo.Rooms (RoomNumber) VALUES (@roomNumber);" +
                                             "SELECT * FROM dbo.Rooms where Id = @@IDENTITY";

        private const string DeleteRoom = "DELETE from dbo.Rooms where Id = @id";

        private const string UpdateRoom = "UPDATE dbo.Rooms SET RoomNumber = @roomNumber where Id = @id";
        private const string GetRoomById = "SELECT * from dbo.Rooms where Id = @id";

        private readonly IGenericDbContext _context;

        public RoomRepository(IGenericDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Room> GetAll()
        {
            var output = _context.LoadData<Room, dynamic>(RoomsLoadAll, new { });
            return output;
        }

        public Room GetById(int id)
        {
            return _context.LoadData<Room, dynamic>(GetRoomById, new { id }).FirstOrDefault();
        }

        public bool Update(int id, Room entity)
        {
            var room = new
            {
                Id = entity.Id,
                RoomNumber = entity.RoomNumber
            };
            var output = _context.SaveData(UpdateRoom, room);
            return output > 0;
        }

        public Room Create(Room entity)
        {
            var newRoom = new
            {
                RoomNumber= entity.RoomNumber
            };
            var room = _context.LoadData<Room, dynamic>(InsertNewRoom, newRoom).FirstOrDefault();
            return room;
        }

        public bool Delete(int id)
        {
            var output = _context.SaveData(DeleteRoom, new {id});
            return output > 0;
        }
    }
}
