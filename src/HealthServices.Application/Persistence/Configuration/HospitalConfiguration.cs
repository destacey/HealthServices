using HealthServices.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthServices.Application.Persistence.Configuration
{
    public class HospitalConfiguration : IEntityTypeConfiguration<Hospital>
    {
        public void Configure(EntityTypeBuilder<Hospital> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(255);
            builder.Property(x => x.CreatedTime);
            builder.Property(x => x.LastModifiedTime);

            // owned properties
            builder.OwnsOne(x => x.Address, xa =>
            {
                xa.Property(a => a.Line1).HasMaxLength(255).IsRequired();
                xa.Property(a => a.Line2).HasMaxLength(255);
                xa.Property(a => a.City).HasMaxLength(255).IsRequired();
                xa.Property(a => a.State).HasMaxLength(255).IsRequired();
                xa.OwnsOne(a => a.ZipCode)
                    .Property(z => z.Value).HasMaxLength(10).IsRequired().HasColumnName("Address_ZipCode");
            });

            builder.OwnsOne(x => x.PhoneNumber)
                .Property(a => a.Value).HasMaxLength(10).IsRequired().HasColumnName("PhoneNumber");
        }
    }
}
