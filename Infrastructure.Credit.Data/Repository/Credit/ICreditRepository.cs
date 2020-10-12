using Domain.Credit.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Credit.Data.Repository
{
    public interface ICreditRepository
    {
        Task<long> Create(CreditEntity credit, ClientEntity client,
            List<QuotaEntity> quotas);
        Task<CreditEntity> GetById(long idCredit);
        Task<List<CreditEntity>> GetByIdClient(long idClient);
    }
}
