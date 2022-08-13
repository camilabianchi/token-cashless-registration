using CashlessRegistration.Application.DTOs;
using CashlessRegistration.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CashlessRegistration.Register.API.Controllers
{
    [ApiController]
    [Route("api/customer-cards")]
    public class CustomerCardsController : ControllerBase
    {
        private readonly ICustomerCardService _customerCardService;

        public CustomerCardsController(ICustomerCardService customerCardService)
        {
            _customerCardService = customerCardService;
        }

        /// <summary>
        /// Save customer card information
        /// </summary>
        /// <param name="customerCardRequestDTO">CustomerCardRequestDTO values</param>
        /// <returns>Customer card data</returns>
        [HttpPost("save")]
        public async Task<ActionResult<CustomerCardResponseDTO>> Save(CustomerCardRequestDTO customerCardRequestDTO)
        {
            var customerCardResponseDTO = await _customerCardService.SaveCard(customerCardRequestDTO);

            return Ok(customerCardResponseDTO);
        }
    }
}