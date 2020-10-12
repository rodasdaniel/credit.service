﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using System;
using System.IO;

namespace Credit.API.App_Start
{
    public class SwaggerConfig
    {
        protected SwaggerConfig() { }

        public static void Register(IServiceCollection services)
        {
            Uri contactUrl = new Uri("http://www.rodasdaniel.co");
            var fileDocumentationPath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "Credit.API.xml");
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Credit Service - Prueba Sistecrédito",
                    Description = "REST Service encargado de realizar las operaciones de los créditos",
                    TermsOfService = contactUrl,
                    Contact = new OpenApiContact() { Name = "Service admin", Email = "daniel.alejandro.rodas@gmail.com", Url = contactUrl }
                });
                c.IncludeXmlComments(fileDocumentationPath);
            });
        }
        public static void Configure(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "Credit Service - Prueba Sistecrédito");
            });
        }
    }
}
