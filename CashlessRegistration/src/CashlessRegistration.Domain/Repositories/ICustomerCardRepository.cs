using CashlessRegistration.Domain.Data;
using CashlessRegistration.Domain.Entities;

namespace CashlessRegistration.Domain.Repositories
{
    public interface ICustomerCardRepository : IRepository<CustomerCard>
    {
        void SaveCard(CustomerCard customerCard);
        Task<CustomerCard> GetByCardId(long id);
        Task<List<CustomerCard>> GetAll();
    }
}
