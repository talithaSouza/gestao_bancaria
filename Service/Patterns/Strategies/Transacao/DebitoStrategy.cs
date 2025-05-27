using Domain.Entidades;

namespace Service.Patterns.Strategies.Transacao
{
    public class DebitoStrategy : ITransacaoStrategy
    {
        public Conta Saque(Conta conta, decimal valorRetirado)
        {
            decimal valorComJuros = valorRetirado + (valorRetirado * (decimal)0.03);

            conta.RealizarSaque(valorComJuros);

            return conta;
        }

    }
}