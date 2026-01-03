using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoGameStore.Domain.Entities;
using VideoGameStore.Domain.ValueObjects;

namespace VideoGameStore.Infrastructure.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired();

        builder.OwnsOne(x => x.WalletBalance, b =>
        {
            b.Property(m => m.Amount)
             .HasColumnName("WalletBalance")
             .HasPrecision(18, 2);
        });

        builder.OwnsOne(x => x.Membership, m =>
        {
            m.Property(p => p.Type).HasColumnName("MembershipType");
        });

        builder.Navigation(x => x.Rentals).UsePropertyAccessMode(PropertyAccessMode.Field);
        builder.Navigation(x => x.Transactions).UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}

