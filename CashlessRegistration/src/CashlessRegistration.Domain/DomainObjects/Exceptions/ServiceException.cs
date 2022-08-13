namespace CashlessRegistration.Domain.DomainObjects.Exceptions
{
    public class ServiceException : Exception
    {
        public ServiceException(string message) : base(message)
        {
        }
    }
}
