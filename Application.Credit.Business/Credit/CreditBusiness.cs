using Application.Credit.Common.Utils;
using Application.Credit.Dtos;
using AutoMapper;
using Domain.Credit.Entities;
using Infrastructure.Credit.Agents;
using Infrastructure.Credit.Data.Repository;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using static Application.Credit.Common.Resources.Messages;


namespace Application.Credit.Business.Credit
{
    public class CreditBusiness : ICreditBusiness
    {
        #region Constructor
        private readonly ICreditRepository _creditRepository;
        private readonly IClientProvider _clientProvider;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public CreditBusiness(ICreditRepository creditRepository,
            IClientProvider clientProvider,
            IMapper mapper,
            IConfiguration config)
        {
            _creditRepository = creditRepository;
            _clientProvider = clientProvider;
            _mapper = mapper;
            _config = config;
        }
        #endregion
        public async Task<(HttpStatusCode statusCode, string message, bool response)>
            Create(CreditDataDto credit)
        {
            InfoClientDto infoClientDto = null;
            (bool valid, HttpStatusCode creditStatus, string creditMsg) =
                CreateCreditValidations(credit, ref infoClientDto);
            if (!valid)
            {
                return (creditStatus, creditMsg, false);
            }
            CreditEntity creditEntity = _mapper.Map<CreditEntity>(credit);
            ClientEntity clientEntity = _mapper.Map<ClientEntity>(infoClientDto);
            List<QuotaEntity> quotaEntities = _mapper.Map<List<QuotaEntity>>(credit.Quotas);
            clientEntity.CupoDisponible = clientEntity.CupoDisponible - credit.CapitalValue;
            creditEntity.FechaCreacion = ColombianHour.GetDate();
            _ = _creditRepository.Create(creditEntity, clientEntity, quotaEntities);
            return (HttpStatusCode.OK, SuccessMsg, true);
        }

        public async Task<(HttpStatusCode statusCode, string message, CreditDataDto response)>
            GetById(long idCredit)
        {
            CreditEntity credit = _creditRepository.GetById(idCredit).Result;
            if (credit == null)
            {
                return (HttpStatusCode.NoContent, CreditNoExistMsg, null);
            }
            CreditDataDto prb = new CreditDataDto();
            prb = _mapper.Map(credit, prb);
            return (HttpStatusCode.OK, SuccessMsg, new CreditDataDto
            {
                IdClient = credit.IdCliente,
                IdCredit = credit.IdCredito,
                TermMonths = credit.Plazo,
                Frequency = credit.Frecuencia,
                CapitalValue = credit.ValorCapital,
                TotalValue = credit.ValorTotal,
                CreationDate = credit.FechaCreacion
            });
        }

        public async Task<(HttpStatusCode statusCode, string message, List<CreditDataDto> response)>
            GetByIdClient(long idClient)
        {
            List<CreditEntity> credits = _creditRepository.GetByIdClient(idClient).Result;
            if (credits == null || credits.Count <= 0)
            {
                return (HttpStatusCode.NoContent, NoCreditsToClientMsg, null);
            }
            List<CreditDataDto> creditList = new List<CreditDataDto>();
            foreach (CreditEntity credit in credits)
            {
                creditList.Add(new CreditDataDto
                {
                    IdClient = credit.IdCliente,
                    IdCredit = credit.IdCredito,
                    TermMonths = credit.Plazo,
                    Frequency = credit.Frecuencia,
                    CapitalValue = credit.ValorCapital,
                    TotalValue = credit.ValorTotal,
                    CreationDate = credit.FechaCreacion
                });
            }
            return (HttpStatusCode.OK, SuccessMsg, creditList);
        }
        #region Private
        private (bool IsValid, HttpStatusCode statusCode, string message)
            ClientValidations(long idClient, decimal capitalValue, ref InfoClientDto infoClientDto)
        {
            infoClientDto = _clientProvider.GetInfoClient(idClient).Result;
            if (infoClientDto == null)
            {
                return (false, HttpStatusCode.BadRequest, ClientNoExistMsg);
            }
            if (infoClientDto.AvailableSpace < capitalValue)
            {
                return (false, HttpStatusCode.PreconditionFailed, NoAvalibleSpaceMsg);
            }
            return (true, HttpStatusCode.OK, string.Empty);
        }
        private bool ValidateTermMonths(CreditDataDto credit)
        {
            List<TermDto> termsDto = _config.GetSection("CommonValues:Terms").Get<List<TermDto>>();
            List<int> allowedTerms = new List<int>();
            foreach (TermDto term in termsDto)
            {
                if ((credit.CapitalValue >= term.From
                    && credit.CapitalValue <= term.To)
                    || credit.CapitalValue > term.To)
                {
                    allowedTerms.Add(term.Months);
                }
            }
            return allowedTerms.Contains(credit.TermMonths);
        }
        private (bool IsValid, HttpStatusCode statusCode, string message)
            QuotasValidations(CreditDataDto credit)
        {
            int quotaQuantity = credit.TermMonths * ((credit.Frequency == 15) ? 2 : 1);
            if (quotaQuantity != credit.Quotas.Count)
            {
                return (false, HttpStatusCode.BadRequest, FrequencyErrorMsg);
            }
            return (true, HttpStatusCode.OK, string.Empty);
        }
        private (bool IsValid, HttpStatusCode statusCode, string message)
            CreateCreditValidations(CreditDataDto credit, ref InfoClientDto infoClientDto)
        {
            (bool clientValid, HttpStatusCode clientStatus, string clientMessage) =
                ClientValidations(credit.IdClient, credit.CapitalValue, ref infoClientDto);
            if (!clientValid)
            {
                return (false, clientStatus, clientMessage);
            }
            if (!ValidateTermMonths(credit))
            {
                return (false, HttpStatusCode.BadRequest, TermNoAllowedMsg);
            }
            (bool quotasValid, HttpStatusCode quotasStatus, string quotasMessage) =
                QuotasValidations(credit);
            if (!quotasValid)
            {
                return (false, quotasStatus, quotasMessage);
            }
            return (true, HttpStatusCode.OK, string.Empty);
        }
        #endregion 

    }
}
