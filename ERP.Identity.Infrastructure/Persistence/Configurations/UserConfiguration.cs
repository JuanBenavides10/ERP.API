using ERP.Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Identity.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UsersEntity>
    {
        public void Configure(EntityTypeBuilder<UsersEntity> builder)
        {
            builder.ToTable("users");

            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.Uuid).IsUnique();

            builder.Property(x => x.Uuid)
                .HasColumnType("uuid")
                .HasDefaultValueSql("gen_random_uuid()")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(x => x.PersonId)
                .IsRequired();

            builder.Property(x => x.UserName)
                .HasMaxLength(50)
                .IsRequired();
            builder.HasIndex(x => x.UserName)
                .IsUnique();

            builder.Property(x => x.PasswordHash)
                .IsRequired()
                .HasColumnType("bytea");

            builder.Property(x => x.PasswordSalt)
                .IsRequired()
                .HasColumnType("bytea");

            builder.Property(x => x.Attempts)
                .HasDefaultValue(0);

            builder.Property(x => x.LockedUntil)
                .IsRequired(false);

            builder.Property(x => x.Active)
                .HasDefaultValue(true);

            builder.Property(x => x.CreatedAt)
                .HasDefaultValueSql("now()");

            //builder.HasOne(x => x.Person)
            //    .WithMany(p => p.Users)
            //    .HasForeignKey(x => x.PersonId)
            //    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
