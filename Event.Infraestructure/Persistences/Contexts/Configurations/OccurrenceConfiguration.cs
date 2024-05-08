using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Event.Domain.Entities;

namespace Event.Infraestructure.Persistences.Contexts.Configurations
{
    public class OccurrenceConfiguration : IEntityTypeConfiguration<Occurrence>
    {
        public void Configure(EntityTypeBuilder<Occurrence> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("OccurrenceId");

            builder.Property(e => e.Description).HasMaxLength(500);

        }
    }
}
