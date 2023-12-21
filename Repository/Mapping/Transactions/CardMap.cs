using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Transactions.Agreggates;
using Domain.Core.ValueObject;
using Domain.Transactions.ValueObject;

namespace Repository.Mapping.Transactions
{
    public class CardMap : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.ToTable(nameof(Card));

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Active).IsRequired();
            builder.Property(x => x.Number).IsRequired().HasMaxLength(19);            
            builder.Property(x => x.CVV).IsRequired().HasMaxLength(255);

            builder.OwnsOne<CreditCardBrandInfo>(e => e.CardBrand, c =>
            {
                c.Ignore(e => e.Brand);
                c.Property(x => x.Name).HasColumnName("Brand").HasMaxLength(12).IsRequired();
            });

            builder.OwnsOne<ExpiryDate>(e => e.Validate, (Action<OwnedNavigationBuilder<Card, ExpiryDate>>)(c =>
            {
                c.Ignore(e => e.Month);
                c.Ignore(e => e.Year);
                c.Property(x => x.Value).HasColumnName("Validate").IsRequired();
            }));

            builder.OwnsOne<Monetary>(d => d.Limit, c =>
            {
                c.Property(x => x.Value).HasColumnName("Limit").IsRequired();
            });

            builder.HasMany(x => x.Transactions).WithOne();

        }
    }
}