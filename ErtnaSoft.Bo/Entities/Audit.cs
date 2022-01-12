using System;

namespace ErtnaSoft.Bo.Entities
{
    public class Audit : Entity
    {
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
    }
}