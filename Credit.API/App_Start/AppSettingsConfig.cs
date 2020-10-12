using Application.Credit.Common.Utils;
using Infrastructure.Credit.Data.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Credit.API.App_Start
{
    public class AppSettingsConfig
    {
        protected AppSettingsConfig() { }

        public static void Register(IServiceCollection services, IHostingEnvironment _env,
            IConfiguration Configuration)
        {
            services.AddDbContext<ApplicationDBContext>(options =>
            options.UseSqlServer(GetEnvVariable.Get(_env, "dbconnection", Configuration)));
        }
    }
}
