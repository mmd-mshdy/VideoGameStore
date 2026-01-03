using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoGameStore.Domain.Entities;

namespace VideoGameStore.Infrastructure.Configurations;

public class RentalConfiguration : IEntityTypeConfiguration<Rental>
{
    public void Configure(EntityTypeBuilder<Rental> builder)
    {
        builder.ToTable("Rentals");
        builder.HasKey(x => x.Id);
    }
}
