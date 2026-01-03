using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoGameStore.Domain.Entities;
using VideoGameStore.Domain.ValueObjects;

namespace VideoGameStore.Infrastructure.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("Transactions");
        builder.HasKey(x => x.Id);

        builder.OwnsOne(x => x.Amount, m =>
        {
            m.Property(p => p.Amount)
             .HasColumnName("Amount")
             .HasPrecision(18, 2);
        });
    }
}
