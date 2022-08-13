using CashlessRegistration.Application.DTOs;

namespace CashlessRegistration.Application.Services.Interfaces
{
    public interface ICustomerCardService
    {
        Task<CustomerCardResponseDTO> SaveCard(CustomerCardRequestDTO customerCardDTO);
        Task<bool> ValidateToken(CustomerCardValidationRequestDTO customerCardValidationRequestDTO);
    }
}
