﻿using System;

namespace ErtnaSoft.Bo.Entities
{
    public class Reservation : Audit
    {
        
        public int Id { get; set; }
        public int RoomReservationId { get; set; }
        public int InvoiceId { get; set; }
        public int NumberOfPeople { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsCheckedIn { get; set; }
        public bool IsCanceled { get; set; }
    }
}
