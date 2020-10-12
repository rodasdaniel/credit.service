using Application.Credit.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Credit.Agents
{
    public interface IQuotaProvider
    {
        Task<bool> Create(List<QuotaDataDto> quotas);
    }
}
