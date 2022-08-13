namespace CashlessRegistration.Domain.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
