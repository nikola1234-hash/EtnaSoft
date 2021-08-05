using System;
using System.Collections.Generic;
using System.Text;
using EtnaSoft.Bo.Entities;
using EtnaSoft.Dal.Infrastucture;

namespace EtnaSoft.Dal.Repositories
{
    public class RoomRepository : IRepository<Room>
    {
        public IEnumerable<Room> GetAll()
        {
            throw new NotImplementedException();
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
