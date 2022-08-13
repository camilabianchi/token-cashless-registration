using Bogus;
using CashlessRegistration.Application.DTOs;
using CashlessRegistration.Domain.Entities;
using CashlessRegistration.UnitTests.Extensions;

namespace CashlessRegistration.UnitTests.Fakers
{
    public static class CustomerCardFaker
    {
        const long CARD_ID = 1;
        const int CUSTOMER_ID = 1;
        const long CARD_NUMBER = 4220126755596383;
        const int CVV = 2;
        const long TOKEN = 8363;

        public static CustomerCardRequestDTO ValidCustomerCardRequestDTO(int cvv = CVV)
            => new Faker<CustomerCardRequestDTO>()
                .RuleFor(f => f.CustomerId, CUSTOMER_ID)
                .RuleFor(f => f.CardNumber, CARD_NUMBER.ToString())
                .RuleFor(f => f.CVV, f => cvv)
                .Generate();

        public static CustomerCardValidationRequestDTO ValidCustomerCardValidationRequestDTO(int cvv = CVV)
            => new Faker<CustomerCardValidationRequestDTO>()
                .RuleFor(f => f.CardId, CARD_ID)
                .RuleFor(f => f.CustomerId, CUSTOMER_ID)
                .RuleFor(f => f.Token, TOKEN)
                .RuleFor(f => f.CVV, f => cvv)
                .Generate();

        public static CustomerCard CustomerCardExpiredToken()
            => new Faker<CustomerCard>()
                .UsePrivateConstructor()
                .RuleFor(f => f.Id, CARD_ID)
                .RuleFor(f => f.CustomerId, CUSTOMER_ID)
                .RuleFor(f => f.CardNumber, CARD_NUMBER)
                .RuleFor(f => f.Token, TOKEN)
                .RuleFor(f => f.RegistrationDate, f => DateTime.UtcNow.AddMinutes(-35))
                .Generate();

        public static CustomerCard CustomerCardWrongOwner()
            => new Faker<CustomerCard>()
                .UsePrivateConstructor()
                .RuleFor(f => f.Id, CARD_ID)
                .RuleFor(f => f.CustomerId, 999)
                .RuleFor(f => f.CardNumber, CARD_NUMBER)
                .RuleFor(f => f.Token, TOKEN)
                .RuleFor(f => f.RegistrationDate, f => DateTime.UtcNow)
                .Generate();

        public static CustomerCard CustomerCard()
            => new Faker<CustomerCard>()
                .UsePrivateConstructor()
                .RuleFor(f => f.Id, CARD_ID)
                .RuleFor(f => f.CustomerId, CUSTOMER_ID)
                .RuleFor(f => f.CardNumber, CARD_NUMBER)
                .RuleFor(f => f.Token, TOKEN)
                .RuleFor(f => f.RegistrationDate, f => DateTime.UtcNow)
                .Generate();
    }
}
