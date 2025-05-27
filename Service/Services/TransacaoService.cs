using Domain.Entidades;
using Domain.Exceptions;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Service;

namespace Services
{
    public class TransacaoService : ITransacaoService
    {
        private readonly ITransacaoRepository _repository;
        private readonly IContaRepository _contaRepository;

        public TransacaoService(ITransacaoRepository repository, IContaRepository contaRepository)
        {
            _repository = repository;
            _contaRepository = contaRepository;

        }
        public async Task<Conta> Pix(int numeroConta, decimal saldoRetirado)
        {
            Conta conta = await _contaRepository.Retornar(numeroConta) ?? throw new NotFoundException($"Não foi encontrado conta com o numero {numeroConta}");

            conta.RealizarSaque(saldoRetirado);

            return await _repository.AtualizarSaldoAsync(conta);
        }

    }
}