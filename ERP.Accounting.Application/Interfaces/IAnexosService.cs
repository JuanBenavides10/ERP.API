using ERP.Accounting.Application.DTOs.Requests;
using ERP.Accounting.Application.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Accounting.Application.Interfaces
{
    public interface IAnexosService
    {
        Task<GetAnexosResponse> CreateAsync(CreateAnexosRequest request);
        Task<GetAnexosResponse?> GetByUuidAsync(Guid uuid);
        Task<List<GetAnexosResponse>> GetAllAsync();
        Task<bool> UpdateAsync(Guid uuid, UpdateAnexosRequest request);
        Task<bool> DeleteAsync(Guid uuid);

    }
}
