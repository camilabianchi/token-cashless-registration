using CashlessRegistration.Domain.Data;
using CashlessRegistration.Domain.Entities;
using CashlessRegistration.Domain.Repositories;
using CashlessRegistration.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CashlessRegistration.Infrastructure.Repositories
{
    public class CustomerCardRepository : ICustomerCardRepository
    {
        private readonly CashlessRegistrationDBContext _dbContext;

        public CustomerCardRepository(CashlessRegistrationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IUnitOfWork UnitOfWork => _dbContext;

        public void SaveCard(CustomerCard customerCard)
        {
            _dbContext.CustomerCards.AddAsync(customerCard);
        }

        public async Task<List<CustomerCard>> GetAll()
        {
            return await _dbContext.CustomerCards.ToListAsync();
        }

        public async Task<CustomerCard> GetByCardId(long id)
        {
            return await _dbContext.CustomerCards.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
