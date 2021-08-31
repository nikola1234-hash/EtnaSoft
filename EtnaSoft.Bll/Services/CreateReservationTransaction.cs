using System;
using System.Linq;
using ErtnaSoft.Bo.Entities;
using EtnaSoft.Bo.Entities;
using EtnaSoft.Dal.Infrastucture;

namespace EtnaSoft.Bll.Services
{


    public class CreateReservationTransaction : ICreateReservationService
    {
        private IDbTransactions _context;
        public CreateReservationTransaction()
        {
            
        }

        public void CreateReservationInTransaction(RoomReservation roomReservation, Reservation reservation)
        {
            
            object CreateRoomReservation(RoomReservation rr)
            {
                var output = new
                {
                    GuestId = rr.GuestId,
                    RoomId = rr.RoomId,
                    StayTypeId = rr.StayTypeId,
                    CreatedBy = rr.CreatedBy
                };
                return output;
            }
            object CreateReservationObject(Reservation res, RoomReservation newRoomRes)
            {
                var output = new
                {
                    RoomReservationId = newRoomRes.Id,
                    NumberOfPeople = res.NumberOfPeople,
                    StartDate = res.StartDate,
                    EndDate = res.EndDate,
                    TotalPrice = res.TotalPrice,
                    CreatedBy = res.CreatedBy

                };
                return output;
            }

            using (_context = new DbTransactions())
            {
                try
                {
                    _context.StartTransaction();
                    var rReservationObject = CreateRoomReservation(roomReservation);
                    var newRoomReservation = _context
                        .LoadDataTransaction<RoomReservation, dynamic>("sp_CreateRoomReservation", rReservationObject)
                        .FirstOrDefault();
                    if (newRoomReservation is null)
                        throw new Exception("Room reservation objekat je null");

                    var reservationObject = CreateReservationObject(reservation, newRoomReservation);
                    _context.SaveDataTransaction("sp_CreateReservation", reservationObject);
                    _context.CommitTransaction();
                }
                catch
                {
                    _context.RollBackTransaction();
                    throw;
                }
            }
       

        }

    }
}
