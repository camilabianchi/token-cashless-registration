using CashlessRegistration.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CashlessRegistration.Infrastructure.EntityConfig
{
    public class CustomerCardConfig : IEntityTypeConfiguration<CustomerCard>
    {
        public void Configure(EntityTypeBuilder<CustomerCard> builder)
        {
            builder.ToTable("customer_cards")
                .HasKey(x => x.Id);

            builder.Property(x => x.CustomerId)
                .HasColumnName("customer_id");

            builder.Property(x => x.CardNumber)
                .HasColumnName("card_number");

            builder.Property(x => x.RegistrationDate)
                .HasColumnName("registration_date");
        }
    }
}
