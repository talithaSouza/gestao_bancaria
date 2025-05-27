using Domain.Interfaces.Repository;
using Domain.Interfaces.Service;
using Infra.Repositories;
using Service.Services;
using Services;

namespace API.Setup
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterService(this IServiceCollection services)
        {
            #region C
            services.AddScoped<IContaService, ContaService>();
            #endregion

            #region M
            services.AddScoped<IMovimentacaoService, MovimentacaoService>();
            #endregion

            #region T
            services.AddScoped<ITransacaoService, TransacaoService>();
            #endregion

            return services;
        }

        public static IServiceCollection RegisterRepository(this IServiceCollection services)
        {
            #region B
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            #endregion

            #region C
            services.AddScoped<IContaRepository, ContaRepository>();
            #endregion
           
            #region M
            services.AddScoped<IMovimentacaoRepository, MovimentacaoRepository>();
            #endregion

            #region T
            services.AddScoped<ITransacaoRepository, TransacaoRepository>();
            #endregion


            return services;
        }
    }
}