using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using EtnaSoft.Dal.Infrastucture;

namespace EtnaSoft.Bll.Services
{
    public class UpdateReservationDateDragService : BookingDragUpdate, IUpdateReservationDateDragService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateReservationDateDragService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public override bool DragUpdateDate(int id, DateTime startDate, DateTime endDate)
        {
            bool success = false;
            var reservation = _unitOfWork.Reservations.GetById(id);
            reservation.StartDate = startDate;
            reservation.EndDate = endDate;
            success = _unitOfWork.Reservations.Update(id, reservation);
            return success;

        }

        public override bool DragUpdateRoom(int id, int roomId)
        {
            bool success = false;
            var reservation = _unitOfWork.Reservations.GetById(id);
            var roomReservation = _unitOfWork.RoomReservations.GetById(reservation.RoomReservationId);
            roomReservation.RoomId = roomId;
            success = _unitOfWork.RoomReservations.Update(roomReservation.Id, roomReservation);
            return success;

        }
    }

  
}
