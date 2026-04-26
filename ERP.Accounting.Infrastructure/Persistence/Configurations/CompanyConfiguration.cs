using ERP.Accounting.Domain.Entities;
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

            builder.HasKey(c => c.id);

            builder.HasIndex(x => x.uuid).IsUnique();

            builder.Property(x => x.uuid)
                   .HasColumnType("uuid")
                   .HasDefaultValueSql("gen_random_uuid()") // PostgreSQL genera el UUID
                   .ValueGeneratedOnAdd()
                   .IsRequired();

            builder.Property(c => c.code)
                   .HasColumnType("char(5)") 
                   .IsRequired();

            builder.Property(c => c.company_name)
                   .HasColumnType("varchar(200)") 
                   .IsRequired();

            builder.Property(c => c.address)
                   .HasColumnType("varchar(250)")
                   .IsRequired();

            builder.Property(c => c.ruc)
                 .HasColumnType("varchar(11)")
                 .IsRequired();

            builder.Property(c => c.email)
                .HasColumnType("varchar(30)");

            builder.Property(c => c.phone)
               .HasColumnType("varchar(20)");

            builder.Property(c => c.logo)
                      .HasColumnType("bytea");

        }
    }
}
