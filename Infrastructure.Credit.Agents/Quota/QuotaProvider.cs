using Application.Credit.Dtos;
using Infrastructure.Credit.Agents.CallService;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Credit.Agents
{
    public class QuotaProvider : IQuotaProvider
    {
        #region Constructor
        private readonly IConfiguration _config;

        public QuotaProvider(IConfiguration config)
        {
            _config = config;
        }
        #endregion 
        public async Task<bool> Create(List<QuotaDataDto> quotas)
        {
            HttpResponseDto<bool> response = (HttpResponseDto<bool>)
                CallRestService.CallServiceAsync<HttpResponseDto<bool>>(
                    _config.GetSection("AgentEndpoints:CreateQuotas").Value
                    , quotas, Method.POST, false, false).Result;
            if (response == null) return false;
            return response.Object;
        }
    }
}
