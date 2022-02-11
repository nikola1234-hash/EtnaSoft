using System;
using System.Collections.Generic;
using System.Text;

namespace ErtnaSoft.Bo.Entities
{
    public class Invoice
    {
        public int Id { get; set; }
        public decimal Avans { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsCanceled { get; set; }
        public DateTime DateGenerated { get; set; }
    }
}
