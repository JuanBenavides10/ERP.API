using ERP.Accounting.Application.Interfaces;
using ERP.Accounting.Domain.Entities;
using ERP.Accounting.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Accounting.Infrastructure.Persistence.Repositories
{
    public class AnexosRepository : IAnexosRepository
    {

        private readonly AppDbContext _db;

        public AnexosRepository(AppDbContext db)
        {
            _db = db;
        }

        public Task<AnexosEntity?> GetByUuidAsync(Guid uuid)
                => _db.Anexos.AsNoTracking().FirstOrDefaultAsync(x => x.UuId == uuid);

        public Task<List<AnexosEntity>> GetAllAsync()
            => _db.Anexos.AsNoTracking().OrderByDescending(x => x.Id).ToListAsync();

        public Task AddAsync(AnexosEntity entity)
            => _db.Anexos.AddAsync(entity).AsTask();

        public void Update(AnexosEntity entity)
            => _db.Anexos.Update(entity);

        public void Remove(AnexosEntity entity)
            => _db.Anexos.Remove(entity);

        public Task<int> SaveChangesAsync()
            => _db.SaveChangesAsync();

    }
}
