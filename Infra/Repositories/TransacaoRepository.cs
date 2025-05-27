using Domain.Entidades;
using Domain.Interfaces.Repository;
using Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class TransacaoRepository : ITransacaoRepository
    {
        private DbSet<Conta> _dataSet;
        private readonly IBaseRepository<Conta> _baseContaRepository;
        public TransacaoRepository(DataContext dataContext, IBaseRepository<Conta> baseContaRepository)
        {
            _dataSet = dataContext.Set<Conta>();
            _baseContaRepository = baseContaRepository;
        }
        public async Task<Conta> AtualizarSaldoAsync(Conta conta)
        {
            return await _baseContaRepository.Editar(conta);
        }

    }
}