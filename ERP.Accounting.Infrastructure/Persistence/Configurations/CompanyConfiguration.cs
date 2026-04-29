using ERP.Accounting.Domain.Entities.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Accounting.Infrastructure.Persistence.Configurations
{
 
        public class CompanyConfiguration : IEntityTypeConfiguration<CompanyEntity>
        {
        public void Configure(EntityTypeBuilder<CompanyEntity> builder)
        {
            builder.ToTable("companies");

            builder.HasKey(c => c.Id);

            builder.HasIndex(x => x.Uuid).IsUnique();

            builder.Property(x => x.Uuid)
                   .HasColumnType("uuid")
                   .HasDefaultValueSql("gen_random_uuid()") // PostgreSQL genera el UUID
                   .ValueGeneratedOnAdd()
                   .IsRequired();

            builder.Property(c => c.Code)
                   .HasColumnType("char(5)") 
                   .IsRequired();

            builder.Property(c => c.CompanyName)
                   .HasColumnType("varchar(120)") 
                   .IsRequired();

            builder.Property(c => c.Address)
                   .HasColumnType("varchar(200)")
                   .IsRequired();

            builder.Property(c => c.Ruc)
                 .HasColumnType("varchar(11)")
                 .IsRequired();

            builder.Property(c => c.Email)
                .HasColumnType("varchar(40)");

            builder.Property(c => c.Phone)
               .HasColumnType("varchar(25)");

            builder.Property(c => c.Logo)
                      .HasColumnType("bytea");

        }
    }
}
