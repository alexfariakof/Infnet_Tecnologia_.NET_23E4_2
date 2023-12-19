using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Transactions.Agreggates;
using Domain.Core.ValueObject;

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
            builder.Property(x => x.Number).IsRequired().HasMaxLength(100);

            builder.OwnsOne<Monetary>(d => d.Limit, c =>
            {
                c.Property(x => x.Value).HasColumnName("Limit").IsRequired();
            });

            builder.HasMany(x => x.Transactions).WithOne();

        }
    }
}