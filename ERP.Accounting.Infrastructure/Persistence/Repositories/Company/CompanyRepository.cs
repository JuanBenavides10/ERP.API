using ERP.Accounting.Application.DTOs.Requests.Company;
using ERP.Accounting.Application.DTOs.Requests.Pagination;
using ERP.Accounting.Application.DTOs.Responses.Pagination;
using ERP.Accounting.Application.Interfaces.Company;
using ERP.Accounting.Domain.Entities;
using ERP.Accounting.Domain.Entities.Company;
using ERP.Accounting.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Accounting.Infrastructure.Persistence.Repositories.Company
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
            return _db.Companies.AsNoTracking().FirstOrDefaultAsync(x => x.Uuid == uuid, ct);
        }
        public void Add(CompanyEntity entity)
        {
            _db.Companies.Add(entity);
        }
        public Task<int> SaveChangesAsync(CancellationToken ct = default) //Confirma los cambios
        {
            return _db.SaveChangesAsync(ct);
        }

        public Task<bool> ExistsByCodeAsync(string code, CancellationToken ct = default)
        {
            return _db.Companies.AsNoTracking().AnyAsync(x => x.Code == code, ct);
        }

        public Task<CompanyEntity?> GetByIdForUpdateAsync(Guid uuid, CancellationToken ct = default)
        {
            return _db.Companies.FirstOrDefaultAsync(x => x.Uuid == uuid, ct);
        }

        public async Task<(int total, List<CompanyEntity> items)> GetPagedEntitiesAsync(CompanyFiltrosRequest filtros,PaginacionRequest paginacion,CancellationToken ct)
        {
            var page = paginacion.Pagina < 1 ? 1 : paginacion.Pagina;
            var size = paginacion.RecordsPorPagina <= 0 ? 10 : paginacion.RecordsPorPagina;
            size = Math.Min(size, 100);

            var query = _db.Companies.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(filtros.CompanyName))
            {
                query = query.Where(x => EF.Functions.ILike(x.CompanyName, $"%{filtros.CompanyName}%"));
            }
            if (!string.IsNullOrWhiteSpace(filtros.Code))
            {
                query = query.Where(x => x.Code == filtros.Code);
            }
            if (!string.IsNullOrWhiteSpace(filtros.Ruc))
            {
                query = query.Where(x => x.Ruc == filtros.Ruc);
            }
        
            var total = await query.CountAsync(ct);

            var items = await query
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync(ct);

            return (total, items);
        }


    }
}
