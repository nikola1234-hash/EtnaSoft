using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using ErtnaSoft.Bo.Entities;
using EtnaSoft.Bo.Entities;
using EtnaSoft.Dal.Infrastucture;

namespace EtnaSoft.Dal.Repositories
{
    public class RoomReservationRepository : IRepository<RoomReservation>
    {
        private const string GetAllRoomRes = "Select * from dbo.RoomReservations";
        private const string GetRoomById = "Select * from dbo.RoomReservations where Id = @Id";
        private const string UpdateRoom = "sp_UpdateRoomReservation";
        private const string CreateRoomReservation = "sp_CreateRoomReservation";
        private readonly IGenericDbContext _context;

        public RoomReservationRepository(IGenericDbContext context)
        {
            _context = context;
        }


        public IEnumerable<RoomReservation> GetAll()
        {
            var output = _context.LoadData<RoomReservation, dynamic>(GetAllRoomRes, new { });
            return output;
        }

        public RoomReservation GetById(int id)
        {
            var output = _context.LoadData<RoomReservation, dynamic>(GetRoomById, new {Id = id}).FirstOrDefault();
            return output;
        }

        public bool Update(int id, RoomReservation entity)
        {
            bool output = false;
            entity.Id = id;
            //var pEntity = new
            //{
            //    Id = entity.Id,
            //    RoomId = entity.RoomId,
            //    StayTypeId = entity.StayTypeId,
            //    GuestId = entity.GuestId,
            //    ModifiedBy = entity.ModifiedBy

            //};
            var parameters = new DynamicParameters(entity);
            var o = _context.SaveData(UpdateRoom, parameters);
            output = o == 1;
            return output;
        }

        public RoomReservation Create(RoomReservation entity)
        {
            var parameters = new DynamicParameters(entity);
            var output = _context.LoadData<RoomReservation, DynamicParameters>(CreateRoomReservation, parameters).FirstOrDefault();
            return output;
        }

        public bool Delete(int id)
        {
            //TODO: Should see how to deal with this
            throw new NotImplementedException();
        }
    }
}
