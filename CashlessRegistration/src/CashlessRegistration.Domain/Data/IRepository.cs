using CashlessRegistration.Domain.DomainObjects;

namespace CashlessRegistration.Domain.Data
{
    public interface IRepository<T>
        where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
