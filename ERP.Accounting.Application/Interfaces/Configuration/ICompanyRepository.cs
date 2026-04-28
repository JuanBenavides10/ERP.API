using ERP.Accounting.Domain.Entities.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Accounting.Application.Interfaces.Configuration
{
    public interface ICompanyRepository
    {
        void Add(CompanyEntity entity);
        Task<bool> ExistsByCodeAsync(string code, CancellationToken ct = default);
        Task<CompanyEntity?> GetByIdAsync(Guid uuid, CancellationToken ct = default);
        Task<int> SaveChangesAsync(CancellationToken ct = default);
    }
}
