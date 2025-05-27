using Domain.Entidades;

namespace Service.Patterns.Strategies.Transacao
{
    public class PixStrategy : ITransacaoStrategy
    {
        public Conta Saque(Conta conta, decimal valorRetirado)
        {
            conta.RealizarSaque(valorRetirado);

            return conta;
        }

    }
}