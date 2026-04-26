using ERP.Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Identity.Infrastructure.Persistence.Configurations
{
    internal class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("persons");

            builder.HasKey(x => x.id);

            builder.Property(x => x.first_name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(x => x.last_name)
                   .HasMaxLength(100);

            builder.Property(x => x.document_type)
                   .HasMaxLength(20);

            builder.Property(x => x.email)
                   .HasMaxLength(100);

            builder.Property(x => x.active)
                   .HasDefaultValue(true);

            builder.Property(x => x.created_at)
                   .HasDefaultValueSql("now()");
        }
    }
}
