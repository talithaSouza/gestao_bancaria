using System.Transactions;
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
        private readonly IMovimentacaoService _movimentacaoService;

        public TransacaoService(ITransacaoRepository repository, IContaRepository contaRepository, IMovimentacaoService movimentacaoService)
        {
            _repository = repository;
            _contaRepository = contaRepository;
            _movimentacaoService = movimentacaoService;
        }

        private static ITransacaoStrategy SetStategy(TipoTransacao tipoTransacao)
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
            using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            Conta conta = await _contaRepository.Retornar(numeroConta) ?? throw new NotFoundException($"Não foi encontrado conta com o numero {numeroConta}");

            decimal saldoAnterior = conta.Saldo;

            var transacaoStrategy = SetStategy(tipoTransacao);
           
            Conta contaAtualizada = transacaoStrategy.Saque(conta, saldoRetirado);

            _ = await _repository.AtualizarSaldoAsync(contaAtualizada);

            await _movimentacaoService.RegistrarAsync(conta.NumeroConta, saldoAnterior, conta.Saldo, tipoTransacao);


            transaction.Complete();

            return contaAtualizada;
        }

    }
}