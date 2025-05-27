using Domain.Entidades;

namespace Domain.Interfaces.Repository
{
    public interface IContaRepository
    {
        public Task<Conta> Criar(Conta conta);
        public Task<Conta> Retornar(int numeroConta);
    }
}