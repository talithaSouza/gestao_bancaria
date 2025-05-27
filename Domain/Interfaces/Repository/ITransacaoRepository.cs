using Domain.Entidades;

namespace Domain.Interfaces.Repository
{
    public interface ITransacaoRepository
    {
        Task<Conta> AtualizarSaldoAsync(Conta conta);
    }
}