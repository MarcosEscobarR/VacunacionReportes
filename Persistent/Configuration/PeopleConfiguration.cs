using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VacunadosReporte.Entities;

namespace VacunadosReporte.Persistent.Configuration
{
    public class PeopleConfiguration: IEntityTypeConfiguration<People>
    {
        public void Configure(EntityTypeBuilder<People> builder)
        {
            builder
                .HasIndex(p => p.DocumentId)
                .IsUnique();
            builder
                .HasIndex(p => p.VaccinationDate);
            builder
                .HasIndex(p => p.UpdatedAt);
            builder
                .HasIndex(p => p.VaccineDescription);
            builder
                .HasIndex(p => p.Dose);
        }
    }
}