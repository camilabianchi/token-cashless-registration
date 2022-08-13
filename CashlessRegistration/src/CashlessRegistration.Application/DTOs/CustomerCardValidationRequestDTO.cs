using System.ComponentModel.DataAnnotations;

namespace CashlessRegistration.Application.DTOs
{
    public class CustomerCardValidationRequestDTO
    {
        [Required(ErrorMessage = "CustomerId is required")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "CardId is required")]
        public long CardId { get; set; }

        [Required(ErrorMessage = "Token is required")]
        public long Token { get; set; }

        [Required(ErrorMessage = "CVV is required")]
        [Range(1, 99999, ErrorMessage = "CVV is not valid")]
        public int CVV { get; set; }
    }
}
