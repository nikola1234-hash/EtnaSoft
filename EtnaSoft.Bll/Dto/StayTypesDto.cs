using System;
using System.Collections.Generic;
using System.Text;
using ErtnaSoft.Bo.Entities;

namespace EtnaSoft.Bll.Dto
{
    public class StayTypesDto : StayType
    {
        public int StayTypeId { get; set; }
        //Number of days promotion include in stay
        public int StayDays  { get; set; }

        //Extra Charge for children
        public decimal ChildPrice { get; set; }
        
    }
}
