using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoGameStore.Domain.Entities;
namespace VideoGameStore.Infrastructure.Configurations
{
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.ToTable("Game");

            builder.HasKey(g => g.Id);

            builder.Property(g => g.Name).IsRequired().HasMaxLength(200);
            builder.Property(g => g.Genre).IsRequired().HasMaxLength(100);
            builder.OwnsOne(g => g.Price, p =>
            {
                p.Property(m => m.Amount).HasColumnName("Price").HasPrecision(18, 2);
            }
            );
            builder.HasOne(g =>g.Inventory).
                WithOne()
                .HasForeignKey<Inventory>("GameId")
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
