using Domain.Entidades;
using Domain.Interfaces.Repository;
using Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class MovimentacaoRepository : IMovimentacaoRepository
    {
        private DbSet<Movimentacao> _dataSet;
        private readonly IBaseRepository<Movimentacao> _baseMovimentacaoRepository;
        public MovimentacaoRepository(DataContext dataContext, IBaseRepository<Movimentacao> baseMovimentacaoRepository)
        {
            _dataSet = dataContext.Set<Movimentacao>();
            _baseMovimentacaoRepository = baseMovimentacaoRepository;
        }
        public async Task<Movimentacao> Inserir(Movimentacao movimentacao)
        {
            return await _baseMovimentacaoRepository.Criar(movimentacao);
        }

        public Task<List<Movimentacao>> RetornaTodasMovimentacoesPorConta(int numeroConta)
        {
            return _dataSet.Where(movi => movi.NumeroConta == numeroConta).ToListAsync();
        }
    }
}