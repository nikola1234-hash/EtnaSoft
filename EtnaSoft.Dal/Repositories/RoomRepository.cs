using System;
using System.Collections.Generic;
using System.Text;
using ErtnaSoft.Bo.Entities;
using EtnaSoft.Dal.Infrastucture;

namespace EtnaSoft.Dal.Repositories
{
    public class RoomRepository : IRepository<Room>
    {
        private const string RoomsLoadAll = "Select * from dbo.Rooms";
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
            throw new NotImplementedException();
        }

        public bool Update(int id, Room entity)
        {
            throw new NotImplementedException();
        }

        public Room Create(Room entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
