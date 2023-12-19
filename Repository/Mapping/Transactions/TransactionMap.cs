using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Transactions.ValueObject;
using Domain.Transactions.Agreggates;

namespace Repository.Mapping.Transactions
{
    public class TransactionMap : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable(nameof(Transaction));

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.DtTransaction).IsRequired();
            builder.Property(x => x.Description).IsRequired().HasMaxLength(50);

            builder.OwnsOne<Merchant>(d => d.Merchant, c =>
            {
                c.Property(x => x.Name).HasColumnName("MerchantNome").IsRequired();
            });

        }
    }
}