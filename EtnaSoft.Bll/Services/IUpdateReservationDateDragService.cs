using System;

namespace EtnaSoft.Bll.Services
{
    public interface IUpdateReservationDateDragService
    {
        bool DragUpdateDate(int id, DateTime startDate, DateTime endDate);
        bool DragUpdateRoom(int id, int roomId);
    }
}