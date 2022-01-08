using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ErtnaSoft.Bo.Entities;
using EtnaSoft.Dal.Infrastucture;
using EtnaSoft.Dal.Stores;

namespace EtnaSoft.Dal.Repositories
{
    public class GuestDataGridRepository : IRepository<DataGridGuest>
    {
          private const string GetAllGuests = "SELECT * from dbo.Guests";
        private const string GetByIdGuest = "Select * from dbo.Guests where Id = @Id";
        private const string CreateGuest = "sp_CreateGuest";

        private const string UpdateGuest = "sp_GuestUpdate";

        

        private readonly IGenericDbContext _context;

        public GuestDataGridRepository(IGenericDbContext context)
        {
            _context = context;
        }

        public IEnumerable<DataGridGuest> GetAll()
        {
            IEnumerable<DataGridGuest> output = _context.LoadData<DataGridGuest, dynamic>(GetAllGuests, new { });
            return output;
        }

        public DataGridGuest GetById(int id)
        {
            DataGridGuest output = _context.LoadData<DataGridGuest, dynamic>(GetByIdGuest, new {Id = id}).FirstOrDefault();
            return output;
        }
        public DataGridGuest Create(DataGridGuest guest)
        {
            object MapGuest()
            {
                var guestParam = new
                {
                    FirstName = guest.FirstName,
                    LastName = guest.LastName,
                    Telephone = guest.Telephone,
                    Address = guest.Address,
                    EmailAddress = guest.EmailAddress,
                    BirthDate = guest.BirthDate,
                    UniqueNumber = guest.UniqueNumber,
                    CreatedBy = UserStore.CurrentUser
                };
                return guestParam;
            }
            var newGuest = MapGuest();
            DataGridGuest createdGuest = _context.LoadData<DataGridGuest, dynamic>(CreateGuest, newGuest).FirstOrDefault();
            return createdGuest;
        }

        public bool Update(int id, DataGridGuest guest)
        {
            bool output = false;
            var guestUpdate = new 
            {
                Id = id,
                FirstName = guest.FirstName,
                LastName = guest.LastName,
                Address = guest.Address,
                EmailAddress = guest.EmailAddress,
                Telephone = guest.Telephone,
                UniqueNumber = guest.UniqueNumber,
                ModifiedBy = UserStore.CurrentUser
            };
            
            int i = _context.SaveData(UpdateGuest, guestUpdate);
            if (i == 1)
            {
                output = true;
            }

            return output;
        }

        public bool Delete(int id)
        {
            bool output = false;

            int i = _context.SaveData($"UPDATE dbo.Guests Set IsActive = 0, ModifiedBy = {UserStore.CurrentUser} WHERE Id = @Id", new { Id = id });
            if (i == 1)
            {
                output = true;
            }

            return output;
        }
    }
    
}
