using System;
using System.Collections.Generic;
using System.Text;

namespace EtnaSoft.Bo.Entities
{
    public class RoomReservation : Audit
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public int StayTypeId { get; set; }
        public int GuestId { get; set; }
    }
}
