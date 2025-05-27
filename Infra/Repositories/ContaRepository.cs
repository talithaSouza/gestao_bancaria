using Domain.Entidades;
using Domain.Interfaces.Repository;
using Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class ContaRepository : IContaRepository
    {
        private DbSet<Conta> _dataSet;
        private readonly IBaseRepository<Conta> _baseContaRepository;
        public ContaRepository(DataContext dataContext, IBaseRepository<Conta> baseContaRepository)
        {
            _dataSet = dataContext.Set<Conta>();
            _baseContaRepository = baseContaRepository;
        }
        public async Task<Conta> Criar(Conta conta)
        {
            return await _baseContaRepository.Criar(conta);
        }

        public async Task<Conta> Retornar(int numeroConta)
        {
            return await _baseContaRepository.Retornar(numeroConta);
        }
    }
}