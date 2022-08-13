namespace CashlessRegistration.Domain.DomainObjects.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message)
        {
        }
    }
}
