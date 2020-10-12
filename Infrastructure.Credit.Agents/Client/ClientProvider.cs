using Application.Credit.Dtos;
using Infrastructure.Credit.Agents.CallService;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System.Threading.Tasks;

namespace Infrastructure.Credit.Agents
{
    public class ClientProvider : IClientProvider
    {
        #region Constructor
        private readonly IConfiguration _config;

        public ClientProvider(IConfiguration config)
        {
            _config = config;
        }
        #endregion 
        public async Task<bool> GetInfoClient(ClientDebitCapitalDto clientDebitCapitalDto)
        {
            HttpResponseDto<bool> response = (HttpResponseDto<bool>)
                CallRestService.CallServiceAsync<HttpResponseDto<bool>>(
                    _config.GetSection("AgentEndpoints:CreateQuotas").Value
                    , clientDebitCapitalDto, Method.POST, false, false).Result;
            if (response == null) return false;
            return response.Object;
        }
        public async Task<InfoClientDto> GetInfoClient(long idClient)
        {
            HttpResponseDto<InfoClientDto> infoClientDto = (HttpResponseDto<InfoClientDto>)
                CallRestService.CallServiceAsync<HttpResponseDto<InfoClientDto>>(
                    string.Format(_config.GetSection("AgentEndpoints:GetInfoClient").Value, idClient)
                    , null, Method.GET, false, false).Result;
            if (infoClientDto == null) return null;
            return infoClientDto.Object;
        }
    }
}
