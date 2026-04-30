using ERP.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.Accounting.Application.DTOs.Requests.Company;
using ERP.Accounting.Application.DTOs.Requests.Pagination;
using ERP.Accounting.Application.DTOs.Responses.Company;
using ERP.Accounting.Application.DTOs.Responses.Pagination;

namespace ERP.Accounting.Application.Interfaces.Company
{
    public interface ICompanyService
    {
        Task<ValidationResult> CreateAsync(CreateCompanyRequest request, CancellationToken ct);
        Task<ValidationResult<GetCompanyResponse>> GetByIdAsync(Guid uuid, CancellationToken ct);
        Task<ValidationResult<PaginacionResponse<GetAllCompanyResponse>>> GetPagedAsync(CompanyFiltrosRequest filtros, PaginacionRequest paginacion, CancellationToken ct);
        Task<ValidationResult> UpdateAsync(Guid uuid, UpdateCompanyRequest request, CancellationToken ct);
    }
}
