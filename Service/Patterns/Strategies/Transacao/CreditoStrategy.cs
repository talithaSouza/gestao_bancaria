using Domain.Entidades;

namespace Service.Patterns.Strategies.Transacao
{
    public class CreditoStrategy : ITransacaoStrategy
    {
        public Conta Saque(Conta conta, decimal valorRetirado)
        {
            decimal valorComJuros = valorRetirado + (valorRetirado * (decimal)0.05);

            conta.RealizarSaque(valorComJuros);

            return conta;
        }

    }
}