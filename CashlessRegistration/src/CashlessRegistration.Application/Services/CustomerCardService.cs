using CashlessRegistration.Application.DTOs;
using CashlessRegistration.Application.Services.Interfaces;
using CashlessRegistration.Domain.DomainObjects.Exceptions;
using CashlessRegistration.Domain.Entities;
using CashlessRegistration.Domain.Repositories;

namespace CashlessRegistration.Application.Services
{
    public class CustomerCardService : ICustomerCardService
    {
        private readonly ICustomerCardRepository _customerCardRepository;

        public CustomerCardService(ICustomerCardRepository customerCardRepository)
        {
            _customerCardRepository = customerCardRepository;
        }

        public async Task<CustomerCardResponseDTO> SaveCard(CustomerCardRequestDTO customerCardRequestDTO)
        {
            var customerCard = new CustomerCard(customerCardRequestDTO.CustomerId, Convert.ToInt64(customerCardRequestDTO.CardNumber));
            customerCard.GenerateToken(customerCardRequestDTO.CVV);

            _customerCardRepository.SaveCard(customerCard);
            var result = await _customerCardRepository.UnitOfWork.Commit();

            if (!result)
            {
                throw new ServiceException("Error saving customer card information");
            }

            CustomerCardResponseDTO customerCardResponseDTO = new CustomerCardResponseDTO
            {
                Id = customerCard.Id,
                Token = customerCard.Token,
                RegistrationDate = customerCard.RegistrationDate,
            };

            return customerCardResponseDTO;
        }

        public async Task<bool> ValidateToken(CustomerCardValidationRequestDTO customerCardValidationRequestDTO)
        {
            var customerCard = await _customerCardRepository.GetByCardId(customerCardValidationRequestDTO.CardId);

            if (customerCard is null)
            {
                throw new ServiceException("Card not found");
            }

            if (customerCard.IsTokenExpired())
            {
                throw new ServiceException("Token is expired");
            }

            if (customerCard.CustomerId != customerCardValidationRequestDTO.CustomerId)
            {
                throw new ServiceException("Card number does not belong to the customer");
            }

            Console.WriteLine(customerCard.CardNumber);

            var validationResult = customerCard.ValidateToken(customerCardValidationRequestDTO.CVV, customerCardValidationRequestDTO.Token);                       

            return validationResult;
        }
    }
}
