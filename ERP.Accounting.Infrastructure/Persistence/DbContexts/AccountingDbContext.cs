using ERP.Accounting.Domain.Entities;
using ERP.Accounting.Domain.Entities.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Accounting.Infrastructure.Persistence.DbContexts
{
    public class AccountingDbContext : DbContext
    {
        public AccountingDbContext(DbContextOptions<AccountingDbContext> options) : base(options)
        {
        }

        // DbSets
        public DbSet<CompanyEntity> Companies { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)   // Aplica TODAS las configuraciones IEntityTypeConfiguration<>
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AccountingDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
