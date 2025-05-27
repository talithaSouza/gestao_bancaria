using Domain.Enums;

namespace Domain.Entidades
{
    public class Movimentacao
    {
        public int Id { get; set; }

        public int NumeroConta { get; set; }

        public decimal SaldoAntes { get; set; }
        public decimal SaldoDepois { get; set; }

        public TipoTransacao TipoTransacao { get; set; }

        public DateTime Data { get; private set; }

        public Movimentacao()
        {
            Data = DateTime.Now;
        }

        //Propriedade Navegação
        public Conta Conta { get; set; }
    }
}