using System;
using EtnaSoft.Bo.Entities;

namespace ErtnaSoft.Bo.Entities
{
    public class Guest : Audit
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Telephone { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }
        public string UniqueNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public bool IsActive { get; set; }
    }
}
