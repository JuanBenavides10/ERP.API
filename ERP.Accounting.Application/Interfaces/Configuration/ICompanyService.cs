using ERP.Shared.Common;
using ERP.Accounting.Application.DTOs.Requests.Configuration;
using ERP.Accounting.Application.DTOs.Responses.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Accounting.Application.Interfaces.Configuration
{
    public interface ICompanyService
    {
        Task<ValidationResult> CreateAsync(CreateCompanyRequest request, CancellationToken ct);
        Task<ValidationResult<GetCompanyResponse>> GetByIdAsync(Guid uuid, CancellationToken ct);
        Task<ValidationResult> UpdateAsync(Guid uuid, UpdateCompanyRequest request, CancellationToken ct);
    }
}
