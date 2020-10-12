using Application.Credit.Business.Credit;
using Infrastructure.Credit.Agents;
using Infrastructure.Credit.Data.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Credit.API.App_Start
{
    public class DependencyInjectionConfig
    {
        protected DependencyInjectionConfig() { }
        public static void Register(IServiceCollection services)
        {
            //Business
            services.AddScoped(typeof(ICreditBusiness), typeof(CreditBusiness));

            //Repository
            services.AddScoped(typeof(ICreditRepository), typeof(CreditRepository));

            //Agents
            services.AddScoped(typeof(IClientProvider), typeof(ClientProvider));
            services.AddScoped(typeof(IQuotaProvider), typeof(QuotaProvider));
        }
    }
}
