using System.Collections.Generic;
using System.Linq;
using ErtnaSoft.Bo.Entities;
using EtnaSoft.Dal.Infrastucture;

namespace EtnaSoft.Dal.Repositories
{
    public sealed class RoomStatusRepository : IRepository<RoomStatus>
    {
        private const string GetAllStatuses = "SELECT * from dbo.RoomStatus";
        private const string GetStatusById = "SELECT * from dbo.RoomStatus where Id = @id";

        private const string UpdateStatus =
            "UPDATE dbo.RoomStatus SET RoomId = @roomId, DateUsed = @dateUsed, IsDirty = @IsDirty, DateCleaned = @dateCleaned WHere Id = @id";

        private const string CreateStatus = "INSERT INTO dbo.RoomStatus RoomId, DateUsed, RoomReservationId VALUES(@roomId, @dateUsed, @roomReservationId)" +
                                            "SELECT * from dbo.RoomStatus WHERE Id = @@IDENTITY";


        private readonly IGenericDbContext _context;

        public RoomStatusRepository(IGenericDbContext context)
        {
            _context = context;
        }

        public IEnumerable<RoomStatus> GetAll()
        {
            return _context.LoadData<RoomStatus, dynamic>(GetAllStatuses, new { });
        }

        public RoomStatus GetById(int id)
        {
            return _context.LoadData<RoomStatus, dynamic>(GetStatusById, new { id }).FirstOrDefault();
        }

        public bool Update(int id, RoomStatus entity)
        {
            var e = new
            {
                Id = id,
                RoomId = entity.RoomId,
                DateUsed =entity.DateUsed,
                DateCleaned = entity.DateCleaned,
                IsDirty = entity.IsDirty
            };

            
            var i = _context.SaveData(UpdateStatus, e);
            
            return i > 0;
        }

        public RoomStatus Create(RoomStatus entity)
        {
            var e = new
            {
                RoomId = entity.RoomId,
                DateUsed =entity.DateUsed,
                DateCleaned = entity.DateCleaned,
                IsDirty = entity.IsDirty,
                RoomReservationId = entity.RoomReservationId
            };
            var newStatus = _context.LoadData<RoomStatus, dynamic>(CreateStatus, e).FirstOrDefault();
            return newStatus;

        }

        public bool Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
