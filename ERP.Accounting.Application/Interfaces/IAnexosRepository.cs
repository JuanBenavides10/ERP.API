using ERP.Accounting.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Accounting.Application.Interfaces
{
    public interface IAnexosRepository
    {
        Task<AnexosEntity?> GetByUuidAsync(Guid uuid);
        Task<List<AnexosEntity>> GetAllAsync();

        Task AddAsync(AnexosEntity entity);
        void Update(AnexosEntity entity);
        void Remove(AnexosEntity entity);

        Task<int> SaveChangesAsync();
    }
}
