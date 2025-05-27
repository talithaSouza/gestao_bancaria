using Domain.Exceptions;

namespace Domain.Entidades
{
    public class Conta
    {
        public int NumeroConta { get; private set; }
        public decimal Saldo { get; private set; }

        //Propriedade Navegação
        public IEnumerable<Movimentacao>? Movimentacoes { get; set; }

        public Conta(int numeroConta, decimal saldo)
        {
            NumeroConta = numeroConta;
            Saldo = saldo;

            ValidarDados();
        }

        private void ValidarDados()
        {
            if (NumeroConta == 0 || NumeroConta <= 0)
            {
                throw new DomainException("Numero da conta não pode ser igual ou menor a zero");
            }

            if (Saldo < 0)
            {
                throw new DomainException("Saldo da conta não pode ser menos que zero");
            }
        }

        public void RealizarSaque(decimal saldoRetirado)
        {
            if (Saldo < saldoRetirado)
            {
                throw new SaldoInsificienteException("Não é possivel realizar essa transação, seu saldo é insuficiente");
            }

            Saldo -= saldoRetirado;
        }
    }


}