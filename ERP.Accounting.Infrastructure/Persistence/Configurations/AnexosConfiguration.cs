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
 
    public class AnexosConfiguration : IEntityTypeConfiguration<AnexosEntity>
    {
        public void Configure(EntityTypeBuilder<AnexosEntity> builder)
        {
            builder.ToTable("Anexos");

            builder.HasKey(c => c.Id);


            builder.HasIndex(x => x.UuId).IsUnique();

            builder.Property(x => x.UuId)
                   .HasColumnType("uuid")
                   .HasDefaultValueSql("gen_random_uuid()") // PostgreSQL genera el UUID
                   .ValueGeneratedOnAdd()
                   .IsRequired();


            builder.Property(c => c.TipoAnexo)
                   .HasColumnType("char(1)") //character
                   .IsRequired();

            builder.Property(c => c.Codigo)
                   .HasColumnType("varchar(20)") //varying (formal)
                   .IsRequired();

            builder.Property(c => c.RazonSocial)
                   .HasColumnType("varchar(250)")
                   .IsRequired();

        }
    }
}
