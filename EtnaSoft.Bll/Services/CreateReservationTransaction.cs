using System;
using System.Linq;
using ErtnaSoft.Bo.Entities;
using EtnaSoft.Dal.Infrastucture;

namespace EtnaSoft.Bll.Services
{


    public class CreateReservationTransaction : ICreateReservationService
    {
        private IDbTransactions _context;
        public CreateReservationTransaction()
        {
            
        }

        private object MapInvoice(Invoice invoice)
        {
            var p = new
            {
                Avans = invoice.Avans,
                SubTotal = invoice.SubTotal,
                TotalPrice = invoice.TotalPrice
            };
            return p;
        }
        public bool CreateReservationInTransaction(RoomReservation roomReservation, Reservation reservation, Invoice invoice)
        {
            bool success = false;
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
            object CreateReservationObject(Reservation res, RoomReservation newRoomRes, Invoice newInvoice)
            {
                var output = new
                {
                    RoomReservationId = newRoomRes.Id,
                    NumberOfPeople = res.NumberOfPeople,
                    StartDate = res.StartDate,
                    EndDate = res.EndDate,
                    InvoiceId = newInvoice.Id,
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

                    //TODO: Generate Invoice
                    var invoiceObject = MapInvoice(invoice);
                    var invoices = _context.LoadDataTransaction<Invoice, dynamic>("sp_CreateInvoice", invoiceObject).FirstOrDefault();
                    var reservationObject = CreateReservationObject(reservation, newRoomReservation, invoices);
                    var i = _context.SaveDataTransaction("sp_CreateReservation", reservationObject);
                    if (i > 0)
                    {
                        success = true;
                    }
                    _context.CommitTransaction();
                }
                catch
                {
                    _context.RollBackTransaction();
                    throw;
                }

                return success;
            }
       

        }

    }
}
