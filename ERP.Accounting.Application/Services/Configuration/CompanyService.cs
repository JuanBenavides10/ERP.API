using AutoMapper;
using ERP.Accounting.Application.DTOs.Requests;
using ERP.Accounting.Application.DTOs.Requests.Configuration;
using ERP.Accounting.Application.DTOs.Responses;
using ERP.Accounting.Application.DTOs.Responses.Configuration;
using ERP.Accounting.Application.Interfaces;
using ERP.Accounting.Application.Interfaces.Configuration;
using ERP.Accounting.Domain.Entities;
using ERP.Accounting.Domain.Entities.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Accounting.Application.Services.Configuration
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CompanyService(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        public async Task<GetCompanyResponse?> GetByIdAsync(Guid uuid, CancellationToken ct)
        {
            var entity = await _companyRepository.GetByIdAsync(uuid, ct);

            if (entity is null) return null;

            return _mapper.Map<GetCompanyResponse>(entity);
        }

        public async Task<GetCompanyResponse> CreateAsync(CreateCompanyRequest request, CancellationToken ct)
        {
            var entity = _mapper.Map<CompanyEntity>(request);

            _companyRepository.Add(entity);
            await _companyRepository.SaveChangesAsync(ct);

            return _mapper.Map<GetCompanyResponse>(entity);
        }
        

    }


}
