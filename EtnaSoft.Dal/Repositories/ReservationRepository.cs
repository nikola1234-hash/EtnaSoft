using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using ErtnaSoft.Bo.Entities;
using EtnaSoft.Dal.Infrastucture;

namespace EtnaSoft.Dal.Repositories
{
    public class ReservationRepository : IRepository<Reservation>
    {

        private const string GetAllRes = "SELECT * from dbo.Reservations";
        private const string GetResById = "SELECT * from dbo.Reservations WHERE Id = @Id";
        private const string UpdateRes = "sp_UpdateReservation";
        private const string CreateRes = "sp_CreateReservation";
        private const string CancelReservation = "UPDATE dbo.Reservations SET IsCanceled = 1 WHERE Id = @Id";


        private readonly IGenericDbContext _context;

        public ReservationRepository(IGenericDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Reservation> GetAll()
        {
            var output = _context.LoadData<Reservation, dynamic>(GetAllRes, new {});
            return output;
        }

        public Reservation GetById(int id)
        {
            var output = _context.LoadData<Reservation, dynamic>(GetResById, new {Id= id}).FirstOrDefault();
            return output;
        }
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns>Returns boolean</returns>
        public bool Update(int id, Reservation entity)
        {  
            //Man Entity and return anonymus type
            object MapEntity(Reservation reservation)
            {
                var param = new
                {
                    Id = reservation.Id,
                    RoomReservationId = reservation.RoomReservationId,
                    NumberOfPeople = reservation.NumberOfPeople,
                    StartDate = reservation.StartDate,
                    EndDate = reservation.EndDate,
                    InvoiceId = reservation.InvoiceId,
                    IsCheckedIn = reservation.IsCheckedIn,
                    IsCanceled = reservation.IsCanceled,
                    ModifiedBy = reservation.ModifiedBy
                };
                return param;
            }
            bool output = false;
            var parameters = MapEntity(entity);
            var i = _context.LoadData<Reservation, dynamic>(UpdateRes, parameters);
            output = i != null;
            
            return output;
        }


        //TODO: DEBUG THIS SOMETHING IS NOT RIGHT
        public Reservation Create(Reservation entity)
        {
            var parameters = new DynamicParameters(entity);
            var reservation = _context.LoadData<Reservation, DynamicParameters>(CreateRes, parameters).FirstOrDefault();
            return reservation;
        }


        /// <summary>
        /// Doesnt actually delete reservation it sets IsCanceled to 1
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True</returns>
        public bool Delete(int id)
        {
            bool output = false;
            int i = _context.SaveData(CancelReservation, new {Id = id});
            output = i == 1;
            return output;
        }
    }
}
