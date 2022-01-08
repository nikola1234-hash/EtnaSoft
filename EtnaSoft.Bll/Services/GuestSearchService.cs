using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ErtnaSoft.Bo.Entities;
using EtnaSoft.Dal.Infrastucture;

namespace EtnaSoft.Bll.Services
{
    public class GuestSearchService : IGuestSearchService
    {
        private readonly IUnitOfWork _unit;

        public GuestSearchService(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public IEnumerable<Guest> GetGuests(string keyword = "")
        {
            List<string> words = new List<string>();
            var guests = _unit.Guests.GetAll();
            IEnumerable<Guest> result;
            if (string.IsNullOrWhiteSpace(keyword))
            {
                result = guests;
                return result;
            }
            
            if (keyword.Contains(" "))
            {
                string[] wordStrings = keyword.Split(' ');
                foreach (var wordString in wordStrings)
                {
                    words.Add(wordString);
                }

                
                if (words.Count == 1)
                {
                    result = guests.Where(s => s.FirstName == words[0] && s.IsActive);
                    return result;
                }
                if (words.Count == 2)
                {
                    result = guests.Where(s => s.FirstName == words[0]&& s.LastName == words[1] && s.IsActive);
                    return result;
                }

                if (words.Count == 3)
                {
                    result = guests.Where(s =>
                        s.FirstName == words[0] && s.LastName == words[1] && s.Telephone == words[2] && s.IsActive);
                    return result;
                }

                if (words.Count == 4)
                {
                    result = guests.Where(s =>
                        s.FirstName == words[0] && s.LastName == words[1] && s.Telephone == words[2] && s.Address == words[3] && s.IsActive);
                    return result;
                }
                if (words.Count == 5)
                {
                    result = guests.Where(s =>
                        s.FirstName == words[0] && s.LastName == words[1] 
                                                && s.Telephone == words[2] 
                                                && s.Address == words[3] 
                                                && s.EmailAddress == words[4]
                                                && s.IsActive);
                    return result;
                }
            }
            
            // default result
            result = guests.Where(s => s.FirstName == keyword && s.IsActive);
            return result;
        }
    }
}
