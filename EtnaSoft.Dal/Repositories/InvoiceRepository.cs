using System;
using System.Collections.Generic;
using System.Text;
using ErtnaSoft.Bo.Entities;
using EtnaSoft.Dal.Infrastucture;

namespace EtnaSoft.Dal.Repositories
{
    public class InvoiceRepository : IRepository<Invoice>
    {
        public IEnumerable<Invoice> GetAll()
        {
            throw new NotImplementedException();
        }

        public Invoice GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(int id, Invoice entity)
        {
            throw new NotImplementedException();
        }

        public Invoice Create(Invoice entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
