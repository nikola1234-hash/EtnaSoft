using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using ErtnaSoft.Bo.Entities;
using EtnaSoft.Bll.Models;
using EtnaSoft.Dal.Infrastucture;

namespace EtnaSoft.Bll.Services
{
    public class GuestService : IGuestService
    {
        private readonly IUnitOfWork _unit;

        public GuestService(IUnitOfWork unit)
        {
            _unit = unit;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword">Search keyword default is null</param>
        /// <returns>IEnumrable of object => Cast it as GuestInfo</returns>
        public ObservableCollection<GuestInfo> GetAllGuestData()
        {
            var guestList = _unit.Guests.GetAll();
            ObservableCollection<GuestInfo> guestInfos = new ObservableCollection<GuestInfo>();
            foreach (Guest guest in guestList)
            {
                GuestInfo guestInfo = new GuestInfo(guest);
                guestInfos.Add(guestInfo);
            }


            return guestInfos;
        }

        
        [Obsolete]
        public ObservableCollection<GuestInfo> LoadByKeyword(string keyword)
        {
            ObservableCollection<GuestInfo> guestInfos = new ObservableCollection<GuestInfo>();
            if (keyword.Contains(' '))
            {
                var searchWords = keyword.Split(' ');
                var guest = _unit.Guests.GetAll()
                    .Where(s => s.FirstName == searchWords[0] && s.LastName == searchWords[1]);
                foreach (var g in guest)
                {
                    GuestInfo newGuest = new GuestInfo(g);
                    guestInfos.Add(newGuest);
                }
            }
            else
            {
                var guest = _unit.Guests.GetAll().Where(s => s.FirstName == keyword);
                foreach (var guest1 in guest)
                {
                    GuestInfo gi = new GuestInfo(guest1);

                    guestInfos.Add(gi);

                }
            }

            return guestInfos;
        }
    }
}
