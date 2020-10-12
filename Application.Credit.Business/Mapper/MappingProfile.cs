using Application.Credit.Dtos;
using AutoMapper;
using Domain.Credit.Entities;

namespace Application.Credit.Business.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreditDataDto, CreditEntity>().ReverseMap()
                .ForMember(dest => dest.IdClient, opt => opt.MapFrom(src => src.IdCliente))
                .ForMember(dest => dest.IdCredit, opt => opt.MapFrom(src => src.IdCredito))
                .ForMember(dest => dest.TermMonths, opt => opt.MapFrom(src => src.Plazo))
                .ForMember(dest => dest.Frequency, opt => opt.MapFrom(src => src.Frecuencia))
                .ForMember(dest => dest.CapitalValue, opt => opt.MapFrom(src => src.ValorCapital))
                .ForMember(dest => dest.TotalValue, opt => opt.MapFrom(src => src.ValorTotal))
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.FechaCreacion))
                .ForAllMembers(opts => opts.PreCondition((src, dest, srcMember)
                 => srcMember != null && !string.IsNullOrWhiteSpace(srcMember?.ToString())));
            CreateMap<CreditEntity, CreditDataDto>().ReverseMap()
               .ForMember(dest => dest.IdCliente, opt => opt.MapFrom(src => src.IdClient))
               .ForMember(dest => dest.IdCredito, opt => opt.MapFrom(src => src.IdCredit))
               .ForMember(dest => dest.Plazo, opt => opt.MapFrom(src => src.TermMonths))
               .ForMember(dest => dest.Frecuencia, opt => opt.MapFrom(src => src.Frequency))
               .ForMember(dest => dest.ValorCapital, opt => opt.MapFrom(src => src.CapitalValue))
               .ForMember(dest => dest.ValorTotal, opt => opt.MapFrom(src => src.TotalValue))
               .ForMember(dest => dest.FechaCreacion, opt => opt.MapFrom(src => src.CreationDate))
               .ForAllMembers(opts => opts.PreCondition((src, dest, srcMember)
                 => srcMember != null && !string.IsNullOrWhiteSpace(srcMember?.ToString())));

            CreateMap<QuotaDataDto, QuotaEntity>().ReverseMap()
                .ForMember(dest => dest.IdQuota, opt => opt.MapFrom(src => src.IdCuota))
                .ForMember(dest => dest.IdCredit, opt => opt.MapFrom(src => src.IdCredito))
                .ForMember(dest => dest.CapitalValue, opt => opt.MapFrom(src => src.ValorCapital))
                .ForMember(dest => dest.TotalValue, opt => opt.MapFrom(src => src.ValorTotal))
                .ForMember(dest => dest.PaymentDate, opt => opt.MapFrom(src => src.FechaPago))
                .ForAllMembers(opts => opts.PreCondition((src, dest, srcMember)
                  => srcMember != null && !string.IsNullOrWhiteSpace(srcMember?.ToString())));
            CreateMap<QuotaEntity, QuotaDataDto>().ReverseMap()
                .ForMember(dest => dest.IdCuota, opt => opt.MapFrom(src => src.IdQuota))
                .ForMember(dest => dest.IdCredito, opt => opt.MapFrom(src => src.IdCredit))
                .ForMember(dest => dest.ValorCapital, opt => opt.MapFrom(src => src.CapitalValue))
                .ForMember(dest => dest.ValorTotal, opt => opt.MapFrom(src => src.TotalValue))
                .ForMember(dest => dest.FechaPago, opt => opt.MapFrom(src => src.PaymentDate))
                .ForAllMembers(opts => opts.PreCondition((src, dest, srcMember)
                  => srcMember != null && !string.IsNullOrWhiteSpace(srcMember?.ToString())));

            CreateMap<InfoClientDto, ClientEntity>().ReverseMap()
                .ForMember(dest => dest.IdClient, opt => opt.MapFrom(src => src.IdCliente))
                .ForMember(dest => dest.AvailableSpace, opt => opt.MapFrom(src => src.CupoDisponible))
                .ForAllMembers(opts => opts.PreCondition((src, dest, srcMember)
                  => srcMember != null && !string.IsNullOrWhiteSpace(srcMember?.ToString())));
            CreateMap<ClientEntity, InfoClientDto>().ReverseMap()
                .ForMember(dest => dest.IdCliente, opt => opt.MapFrom(src => src.IdClient))
                .ForMember(dest => dest.CupoDisponible, opt => opt.MapFrom(src => src.AvailableSpace))
                .ForAllMembers(opts => opts.PreCondition((src, dest, srcMember)
                  => srcMember != null && !string.IsNullOrWhiteSpace(srcMember?.ToString())));
            
        }
    }
}
