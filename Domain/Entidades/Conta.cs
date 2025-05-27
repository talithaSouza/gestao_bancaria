using Domain.Exceptions;

namespace Domain.Entidades
{
    public class Conta
    {
        public int NumeroConta { get; set; }
        public double Saldo { get; set; }

        public Conta(int numeroConta, double saldo)
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
    }


}