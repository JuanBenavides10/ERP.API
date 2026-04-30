using ERP.Accounting.Application.DTOs.Requests.Company;
using ERP.Accounting.Application.DTOs.Requests.Pagination;
using ERP.Accounting.Application.DTOs.Responses.Company;
using ERP.Accounting.Application.DTOs.Responses.Pagination;
using ERP.Accounting.Domain.Entities.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Accounting.Application.Interfaces.Company
{
    public interface ICompanyRepository
    {
        void Add(CompanyEntity entity);
        Task<bool> ExistsByCodeAsync(string code, CancellationToken ct = default);
        Task<CompanyEntity?> GetByIdAsync(Guid uuid, CancellationToken ct = default);
        Task<CompanyEntity?> GetByIdForUpdateAsync(Guid uuid, CancellationToken ct = default);
        Task<(int total, List<CompanyEntity> items)> GetPagedEntitiesAsync(CompanyFiltrosRequest filtros, PaginacionRequest paginacion, CancellationToken ct);
        Task<int> SaveChangesAsync(CancellationToken ct = default);

    
    }
}
