namespace Domain.Exceptions
{
    public class ConflitoContaException : Exception
    {
        public ConflitoContaException()
        {

        }

        public ConflitoContaException(string msg) : base(msg)
        {

        }

        public ConflitoContaException(string msg, Exception innerException) : base(msg, innerException)
        {

        }
    }
}