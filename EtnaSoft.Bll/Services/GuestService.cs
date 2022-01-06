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
        public ObservableCollection<GuestInfo> GetAllGuestData(string keyword = null)
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
    }
}
