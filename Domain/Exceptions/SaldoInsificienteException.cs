namespace Domain.Exceptions
{
    public class SaldoInsificienteException : Exception
    {
        public SaldoInsificienteException()
        {

        }

        public SaldoInsificienteException(string msg) : base(msg)
        {

        }

        public SaldoInsificienteException(string msg, Exception innerException) : base(msg, innerException)
        {

        }
    }
}