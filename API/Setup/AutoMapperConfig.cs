using API.DTO;
using AutoMapper;
using Domain.Entidades;
using Domain.Enums;
using Domain.Extensions;

namespace API.Setup
{
    public static class AutoMapperConfig
    {
        public static MapperConfiguration autoMapperConfig;
        internal static IMapper Mapper { get; private set; }

        public static void RegisterAutoMapper(this IServiceCollection services)
        {
            autoMapperConfig = new MapperConfiguration(cgf =>
            {
                #region  C - Maps
                //Conta
                cgf.CreateMap<Conta, ContaDTO>().ReverseMap();
                #endregion

                #region  M - Maps
                //Movimentacao
                cgf.CreateMap<Movimentacao, MovimentacaoDTO>()
                   .ForMember(dest => dest.TipoTransacao, opt => opt.MapFrom(scr => scr.TipoTransacao.GetDescription()))
                   .ReverseMap()
                   .ForMember(dest => dest.TipoTransacao, opt => opt.MapFrom(src => src.TipoTransacao.GetEnumFromDescription<TipoTransacao>()));
                #endregion

            });

            services.AddSingleton(autoMapperConfig.CreateMapper());
        }
    }
}