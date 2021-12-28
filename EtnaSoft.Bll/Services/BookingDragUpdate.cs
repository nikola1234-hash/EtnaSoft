using System;
using System.Collections.Generic;
using System.Text;

namespace EtnaSoft.Bll.Services
{
    public abstract class BookingDragUpdate
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Reservation Id</param>
        /// <param name="startDate">New StartDate</param>
        /// <param name="endDate">New EndDate</param>
        /// <returns>Boolean</returns>
        public abstract bool DragUpdateDate(int id, DateTime startDate, DateTime endDate);
        /// <summary>
        /// Updates room when dragged on schedular to another resource
        /// </summary>
        /// <param name="id">reservation id</param>
        /// <param name="roomId"> room id(resource id)</param>
        /// <returns>Boolean</returns>
        public abstract bool DragUpdateRoom(int id, int roomId);
    }
}
