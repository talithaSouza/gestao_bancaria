using Domain.Entidades;

namespace Domain.Interfaces.Service
{
    public interface IContaService
    {
        public Task<Conta> CriarAsync(Conta novaConta);
    }
}