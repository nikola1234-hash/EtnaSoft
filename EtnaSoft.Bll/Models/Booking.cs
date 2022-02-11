using System;


namespace EtnaSoft.Bll.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public string RoomNumber { get; set; }
        public int StayTypeId { get; set; }
        public string Title { get; set; }
        public int GuestId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string  UniqueNumber { get; set; }

        public string Address { get; set; }
        public int NumberOfPeople { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int InvoiceId { get; set; }
        public bool IsCheckedIn { get; set; }
        public bool IsCanceled { get; set; }
        public bool AllDay { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public int LabelId { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
