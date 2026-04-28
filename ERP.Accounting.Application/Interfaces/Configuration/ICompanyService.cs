using ERP.Accounting.Application.Contracts;
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
        Task<ValidationResponse> CreateAsync(CreateCompanyRequest request, CancellationToken ct);
        Task<ValidationResponse<GetCompanyResponse>> GetByIdAsync(Guid uuid, CancellationToken ct);
    }
}
