using Application.Credit.Dtos;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Application.Credit.Business.Credit
{
    public interface ICreditBusiness
    {
        Task<(HttpStatusCode statusCode, string message, bool response)>
            Create(CreditDataDto credit);
        Task<(HttpStatusCode statusCode, string message, CreditDataDto response)>
            GetById(long idCredit);
        Task<(HttpStatusCode statusCode, string message, List<CreditDataDto> response)>
            GetByIdClient(long idClient);
    }
}
