using Domain.Entidades;
using Domain.Exceptions;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Service;

namespace Service.Services
{
    public class ContaService : IContaService
    {
        private readonly IContaRepository _repository;

        public ContaService(IContaRepository repository)
        {
            _repository = repository;
        }

        public async Task<Conta> CriarAsync(Conta novaConta)
        {
            var contaCadastrada = await _repository.Retornar(novaConta.NumeroConta);

            if (contaCadastrada != null)
                throw new ConflitoContaException($"Já existe uma conta cadastrada com a numeração {novaConta.NumeroConta}");

            return await _repository.Criar(novaConta);
        }
        
        public async Task<Conta> RetornarAsync(int numeroConta)
        {
            Conta conta = await _repository.Retornar(numeroConta);

            if(conta == null)
                throw new NotFoundException($"Não foi encontrada uma conta com o número {numeroConta}");

            return conta;
        }

    }
}