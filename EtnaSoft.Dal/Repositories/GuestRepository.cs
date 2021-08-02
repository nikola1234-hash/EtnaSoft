using System.Collections.Generic;
using System.Linq;
using Dapper;
using EtnaSoft.Bo.Entities;
using EtnaSoft.Dal.Infrastucture;

namespace EtnaSoft.Dal.Repositories
{
    public class GuestRepository
    {

        private const string GetAllGuests = "SELECT * from dbo.Guest";
        private const string GetByIdGuest = "Select * from dbo.Guest where Id = @Id";
        private const string CreateGuest = "Sp_GuestCreate";

        private const string UpdateGuest =
            "UPDATE dbo.Guest SET FirstName = @FirstName, LastName = @LastName, Telephone = @Telephone, EmailAddress = @EmailAddress," +
            "Address = @Address, UniqueNumber = @UniqueNumber, BirthDate = @BirthDate" +
            "WHERE Id = @Id";

        private const string DeleteGuest = "UPDATE dbo.Guest Set IsActive = 0 WHERE Id = @Id";

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

        public Guest GetyId(int id)
        {
            Guest output = _context.LoadData<Guest, dynamic>(GetByIdGuest, new {Id = id}).FirstOrDefault();
            return output;
        }
        public Guest Create(Guest guest)
        {
            DynamicParameters guestToCreate = new DynamicParameters(guest);
                        Guest createdGuest = _context.LoadData<Guest, dynamic>(CreateGuest, guestToCreate).FirstOrDefault();
            return createdGuest;
        }

        public bool Update(int id, Guest guest)
        {
            bool output = false;
            guest.Id = id;
            DynamicParameters guestUpdate = new DynamicParameters(guest);
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
