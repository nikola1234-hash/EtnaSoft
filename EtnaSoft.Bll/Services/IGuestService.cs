using System.Collections.Generic;
using System.Collections.ObjectModel;
using EtnaSoft.Bll.Models;

namespace EtnaSoft.Bll.Services
{
    public interface IGuestService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword">Search keyword default is null</param>
        /// <returns>IEnumrable of object => Cast it as GuestInfo</returns>
        ObservableCollection<GuestInfo> GetAllGuestData();

        ObservableCollection<GuestInfo> LoadByKeyword(string keyword);
    }
}