using Application.Credit.Business.Credit;
using Application.Credit.Dtos;
using AutoMapper;
using Credit.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System;
using System.IO;
using Application.Credit.Business.Mapper;

namespace Credit.Service.Test
{
    public class CreditControllerTest
    {
        #region Constructor

        CreditController _controller;
        ICreditBusiness _service;


        public CreditControllerTest()
        {

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            _service = new CreditBusinessTest(mapper, configuration);
            _controller = new CreditController(_service);
        }
        #endregion 
        [Fact]
        public void Create()
        {
            #region MockValues
            List<QuotaDataDto> quotasMock = new List<QuotaDataDto>();
            for (int i = 1; i <= 4; i++)
            {
                quotasMock.Add(new QuotaDataDto
                {
                    IdQuota = i,
                    IdCredit = 1,
                    CapitalValue = 22500,
                    TotalValue = 23625,
                    PaymentDate = DateTime.Now
                });
            }
            CreditDataDto creditMock = new CreditDataDto
            {
                IdClient = 1,
                CapitalValue = 90000,
                Frequency = 15,
                TermMonths = 2,
                TotalValue = 94500,
                Quotas = quotasMock
            };
            #endregion
            var actionResult = _controller.Create(creditMock).Result as ObjectResult;
            var result = actionResult.Value as HttpResponseDto<bool>;
            Assert.NotNull(result);
            Assert.Equal(200, result.Code);
            var response = result.Object;
            Assert.IsType<bool>(response);
            Assert.True(response);
        }
        [Fact]
        public void GetById()
        {
            var actionResult = _controller.GetById(1).Result as ObjectResult;
            var result = actionResult.Value as HttpResponseDto<CreditDataDto>;
            Assert.NotNull(result);
            Assert.Equal(200, result.Code);
            var response = result.Object;
            Assert.NotNull(response);
            Assert.IsType<CreditDataDto>(response);
        }
        [Fact]
        public void GetByIdClient()
        {
            var actionResult = _controller.GetByIdClient(1).Result as ObjectResult;
            var result = actionResult.Value as HttpResponseDto<List<CreditDataDto>>;
            Assert.NotNull(result);
            Assert.Equal(200, result.Code);
            var response = result.Object;
            Assert.NotNull(response);
            Assert.IsType<List<CreditDataDto>>(response);
            Assert.True(response.Count > 0);
        }
    }
}
