using Application.Credit.Dtos;
using System.Threading.Tasks;

namespace Infrastructure.Credit.Agents
{
    public interface IClientProvider
    {
        Task<bool> GetInfoClient(ClientDebitCapitalDto clientDebitCapitalDto);
        Task<InfoClientDto> GetInfoClient(long idClient);
    }
}
