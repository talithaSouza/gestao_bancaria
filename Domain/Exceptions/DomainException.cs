namespace Domain.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException()
        {

        }

        public DomainException(string msg) : base(msg)
        {

        }

        public DomainException(string msg, Exception innerException) : base(msg, innerException)
        {

        }
    }
}