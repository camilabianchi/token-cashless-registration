namespace CashlessRegistration.Application.DTOs
{
    public class CustomerCardResponseDTO
    {
        public long Id { get; set; }
        public long Token { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
