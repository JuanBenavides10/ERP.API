using ERP.Accounting.Application.Interfaces.Configuration;
using ERP.Accounting.Domain.Entities;
using ERP.Accounting.Domain.Entities.Configuration;
using ERP.Accounting.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Accounting.Infrastructure.Persistence.Repositories.Configuration
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly AccountingDbContext _db;
        public CompanyRepository(AccountingDbContext db)
        {
            _db = db; 
        }
        public Task<CompanyEntity?> GetByIdAsync(Guid uuid, CancellationToken ct = default) //AsNoTracking -> solo lectura
        {
           return _db.Companies.AsNoTracking().FirstOrDefaultAsync(x => x.uuid == uuid,ct);
        }
        public void Add(CompanyEntity entity)
        {
            _db.Companies.Add(entity);
        }
        public Task<int> SaveChangesAsync(CancellationToken ct = default) //Confirma los cambios
        {
            return _db.SaveChangesAsync(ct);
        }
     
    }
}
