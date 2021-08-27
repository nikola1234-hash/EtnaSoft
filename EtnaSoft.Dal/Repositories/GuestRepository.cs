using System.Collections.Generic;
using System.Linq;
using Dapper;
using ErtnaSoft.Bo.Entities;
using EtnaSoft.Bo.Entities;
using EtnaSoft.Dal.Infrastucture;
using Microsoft.AspNet.Identity;

namespace EtnaSoft.Dal.Repositories
{
    
    public class GuestRepository : IRepository<Guest>
    {

        private const string GetAllGuests = "SELECT * from dbo.Guests";
        private const string GetByIdGuest = "Select * from dbo.Guests where Id = @Id";
        private const string CreateGuest = "sp_CreateGuest";

        private const string UpdateGuest = "sp_GuestUpdate";

        private const string DeleteGuest = "UPDATE dbo.Guests Set IsActive = 0 WHERE Id = @Id";

        private readonly IGenericDbContext _context;

        public GuestRepository(IGenericDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Guest> GetAll()
        {
            IEnumerable<Guest> output = _context.LoadData<Guest, dynamic>(GetAllGuests, new { });
            return output;
        }

        public Guest GetById(int id)
        {
            Guest output = _context.LoadData<Guest, dynamic>(GetByIdGuest, new {Id = id}).FirstOrDefault();
            return output;
        }
        public Guest Create(Guest guest)
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
                    CreatedBy = guest.CreatedBy
                };
                return guestParam;
            }
            var newGuest = MapGuest();
            Guest createdGuest = _context.LoadData<Guest, dynamic>(CreateGuest, newGuest).FirstOrDefault();
            return createdGuest;
        }

        public bool Update(int id, Guest guest)
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
                ModifiedBy = guest.ModifiedBy
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

            int i = _context.SaveData(DeleteGuest, new { Id = id });
            if (i == 1)
            {
                output = true;
            }

            return output;
        }
    }

  
}
