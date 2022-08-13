using CashlessRegistration.Application.DTOs;
using CashlessRegistration.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CashlessRegistration.Token.API.Controllers
{
    [ApiController]
    [Route("api/tokens")]
    public class TokensController : ControllerBase
    {
        private readonly ICustomerCardService _customerCardService;

        public TokensController(ICustomerCardService customerCardService)
        {
            _customerCardService = customerCardService;
        }

        /// <summary>
        /// Validate token
        /// </summary>
        /// <param name="customerCardValidationRequestDTO">CustomerCardValidationRequestDTO values</param>
        /// <returns>True or false</returns>
        [HttpPost("validate")]
        public async Task<ActionResult<bool>> Validate(CustomerCardValidationRequestDTO customerCardValidationRequestDTO)
        {
            var validation = await _customerCardService.ValidateToken(customerCardValidationRequestDTO);

            return Ok(validation);
        }
    }
}