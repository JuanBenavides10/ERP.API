using ERP.Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Identity.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            builder.ToTable("users");

            builder.HasKey(x => x.id);

            builder.Property(x => x.person_id)
                .IsRequired();

            builder.Property(x => x.user_name)
                .HasMaxLength(50)
                .IsRequired();
            builder.HasIndex(x => x.user_name)
                .IsUnique();

            builder.Property(x => x.password_hash)
                .IsRequired()
                .HasColumnType("bytea");

            builder.Property(x => x.password_salt)
                .IsRequired()
                .HasColumnType("bytea");

            builder.Property(x => x.attempts)
                .HasDefaultValue(0);

            builder.Property(x => x.locked_until)
                .IsRequired(false);

            builder.Property(x => x.active)
                .HasDefaultValue(true);

            builder.Property(x => x.created_at)
                .HasDefaultValueSql("now()");

            builder.HasOne(x => x.person)
                .WithMany(p => p.users)
                .HasForeignKey(x => x.person_id)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
