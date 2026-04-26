using AutoMapper;
using ERP.Accounting.Application.DTOs.Requests;
using ERP.Accounting.Application.DTOs.Responses;
using ERP.Accounting.Application.Interfaces;
using ERP.Accounting.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Accounting.Application.Services
{
    public class AnexosService : IAnexosService
    {

        private readonly IAnexosRepository _repo;
        private readonly IMapper _mapper;

        public AnexosService(IAnexosRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }


        public async Task<GetAnexosResponse> CreateAsync(CreateAnexosRequest request)
        {
            var entity = _mapper.Map<AnexosEntity>(request);

            // Si NO usas default en DB, descomenta:
            // entity.UuId = Guid.NewGuid();          

            await _repo.AddAsync(entity);
            await _repo.SaveChangesAsync();

            return _mapper.Map<GetAnexosResponse>(entity);
        }

        public async Task<bool> UpdateAsync(Guid uuid, UpdateAnexosRequest request)
        {
         
            var entity = await _repo.GetByUuidAsync(uuid);
            if (entity is null) return false;

            _mapper.Map(request, entity); 

            await _repo.SaveChangesAsync();
            return true;
        }

        public async Task<GetAnexosResponse?> GetByUuidAsync(Guid uuid)
        {
            var entity = await _repo.GetByUuidAsync(uuid);
            return entity is null ? null : _mapper.Map<GetAnexosResponse>(entity);
        }

        public async Task<List<GetAnexosResponse>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            return _mapper.Map<List<GetAnexosResponse>>(list);
        }

    

        public async Task<bool> DeleteAsync(Guid uuid)
        {
            var entity = await _repo.GetByUuidAsync(uuid);
            if (entity is null) return false;

            _repo.Remove(entity);
            await _repo.SaveChangesAsync();

            return true;
        }


    }
}
