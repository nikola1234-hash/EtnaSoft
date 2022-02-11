using System;
using System.Collections.Generic;
using System.Text;

namespace ErtnaSoft.Bo.Entities
{
    public class Company : Entity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BankName { get; set; }
        public string BankAccount { get; set; }
        public string BankName2 { get; set; }
        public string BankAccount2 { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string Telephone2 { get; set; }
        public string Email { get; set; }
        public byte[] ImageLogo { get; set; }
        public string Pib { get; set; }
        public string MaticniBroj { get; set; }


    }
}
