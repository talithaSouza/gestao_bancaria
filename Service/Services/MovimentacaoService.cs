using Domain.Entidades;
using Domain.Enums;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Service;

namespace Service.Services
{
    public class MovimentacaoService : IMovimentacaoService
    {
        private readonly IMovimentacaoRepository _repository;

        public MovimentacaoService(IMovimentacaoRepository repository)
        {
            _repository = repository;
        }


        public async Task RegistrarAsync(int numeroConta, decimal saldoAntes, decimal saldoDepois, TipoTransacao tipoTransacao)
        {
            var movimentacaoBancaria = new Movimentacao()
            {
                NumeroConta = numeroConta,
                SaldoAntes = saldoAntes,
                SaldoDepois = saldoDepois,
                TipoTransacao = tipoTransacao,
            };

            _ = await _repository.Inserir(movimentacaoBancaria);
        }

        public async Task<List<Movimentacao>> RetornarMovimentacoesPorConta(int numeroConta)
        {
            return await _repository.RetornaTodasMovimentacoesPorConta(numeroConta);
        }

    }
}