using Domain.Entidades;

namespace Service.Patterns.Strategies.Transacao
{
    public interface ITransacaoStrategy
    {
        Conta Saque(Conta conta, decimal valorRetirado);
    }
}