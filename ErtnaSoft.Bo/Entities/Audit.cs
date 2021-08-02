using System;

namespace EtnaSoft.Bo.Entities
{
    public class Audit
    {
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
    }
}