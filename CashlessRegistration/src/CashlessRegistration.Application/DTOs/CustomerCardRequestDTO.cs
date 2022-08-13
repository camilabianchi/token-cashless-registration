using System.ComponentModel.DataAnnotations;

namespace CashlessRegistration.Application.DTOs
{
    public class CustomerCardRequestDTO
    {
        [Required(ErrorMessage = "CustomerId is required")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "CardNumber is required")]
        [CreditCard(ErrorMessage = "Card number is not valid")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "CVV is required")]
        [Range(1, 99999, ErrorMessage = "CVV is not valid")]
        public int CVV { get; set; }
    }
}
