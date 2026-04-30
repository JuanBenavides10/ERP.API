using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Accounting.Application.Interfaces.Storage
{
    public interface IFileStorage
    {
        Task DeleteAsync(string? relativePath, CancellationToken ct);
        Task<string> SaveAsync(IFormFile file, string folder, CancellationToken ct);
    }
}
