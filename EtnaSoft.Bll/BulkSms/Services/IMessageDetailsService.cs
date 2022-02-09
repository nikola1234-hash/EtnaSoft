using System.Collections.Generic;
using System.Threading.Tasks;
using EtnaSoft.Bll.Dto;

namespace EtnaSoft.Bll.BulkSms.Services
{
    public interface IMessageDetailsService
    {
        Task<List<MessageDetailsDto>> ReturnMessageDetailsAsync();
    }
}