using Application.Credit.Dtos;
using AutoMapper;
using Domain.Credit.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Credit.API.App_Start
{
    public class AutoMapperConfig
    {
        public static void Register(IServiceCollection services)
        {
            //var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            //IMapper mapper = mapperConfig.CreateMapper();
            //services.AddSingleton(mapper);
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}
