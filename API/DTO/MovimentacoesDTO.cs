namespace API.DTO
{
    public class MovimentacaoDTO
    {
         public int Id { get; set; }

        public int NumeroConta { get; set; }

        public decimal SaldoAntes { get; set; }
        public decimal SaldoDepois { get; set; }

        public string TipoTransacao { get; set; }

        public DateTime Data { get; set; }
    }
}