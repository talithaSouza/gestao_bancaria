using Domain.Entidades;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Service;
using Service.Patterns.Strategies.Transacao;

namespace Services
{
    public class TransacaoService : ITransacaoService
    {
        private readonly ITransacaoRepository _repository;
        private readonly IContaRepository _contaRepository;
        private readonly ITransacaoStrategy transacaoStrategy;

        public TransacaoService(ITransacaoRepository repository, IContaRepository contaRepository)
        {
            _repository = repository;
            _contaRepository = contaRepository;

        }

        private ITransacaoStrategy SetStategy(TipoTransacao tipoTransacao)
        {
            switch (tipoTransacao)
            {
                case TipoTransacao.P:
                    return new PixStrategy();

                case TipoTransacao.C:
                    return new CreditoStrategy();

                case TipoTransacao.D:
                    return new DebitoStrategy();

                default:
                    throw new InvalidOperationException($"Opção de transação {tipoTransacao} inválida");
            }

        }
        
        public async Task<Conta> ExecutarSaquesAsync(TipoTransacao tipoTransacao, int numeroConta, decimal saldoRetirado)
        {
            Conta conta = await _contaRepository.Retornar(numeroConta) ?? throw new NotFoundException($"Não foi encontrado conta com o numero {numeroConta}");

            var transacaoStrategy = SetStategy(tipoTransacao);

            Conta contaAtualizada = transacaoStrategy.Saque(conta, saldoRetirado);

            return await _repository.AtualizarSaldoAsync(contaAtualizada);
        }

    }
}