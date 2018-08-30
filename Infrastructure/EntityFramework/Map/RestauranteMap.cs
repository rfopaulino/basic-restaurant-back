using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Map
{
    public class RestauranteMap : IEntityTypeConfiguration<Restaurante>
    {
        public void Configure(EntityTypeBuilder<Restaurante> builder)
        {
            builder
                .HasMany(x => x.Prato)
                .WithOne(x => x.Restaurante)
                .HasForeignKey(x => x.IdRestaurante);
        }
    }
}
