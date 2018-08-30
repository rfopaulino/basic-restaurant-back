using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Map
{
    public class PratoMap : IEntityTypeConfiguration<Prato>
    {
        public void Configure(EntityTypeBuilder<Prato> builder)
        {

        }
    }
}
