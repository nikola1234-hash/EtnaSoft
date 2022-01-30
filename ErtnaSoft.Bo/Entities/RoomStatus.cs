using System;

namespace ErtnaSoft.Bo.Entities
{
    public class RoomStatus : Entity
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public int RoomReservationId { get; set; }
        public DateTime DateUsed { get; set; }
        public bool IsDirty { get; set; }
        public DateTime? DateCleaned { get; set; }
    }
}
