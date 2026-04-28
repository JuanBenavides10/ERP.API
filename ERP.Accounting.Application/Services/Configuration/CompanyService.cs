using AutoMapper;
using ERP.Accounting.Application.Contracts;
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

        public async Task<ValidationResponse<GetCompanyResponse>> GetByIdAsync(Guid uuid, CancellationToken ct)
        {
            if (uuid == Guid.Empty)
            {
                return ValidationResponse<GetCompanyResponse>.Failure("El identificador de la compañía es inválido.");
            }
            var entity = await _companyRepository.GetByIdAsync(uuid, ct);

            if (entity == null)
            {
                return ValidationResponse<GetCompanyResponse>.Failure("La compañía consultada no se encuentra registrada en el sistema.");
            }

            var dto = _mapper.Map<GetCompanyResponse>(entity);

            return ValidationResponse<GetCompanyResponse>.Success(dto, "Compañía obtenida correctamente");
        }

        public async Task<ValidationResponse> CreateAsync(CreateCompanyRequest request, CancellationToken ct)
        {      
            var exists = await _companyRepository.ExistsByCodeAsync(request.code, ct);

            if (exists)
            {
                return ValidationResponse.Failure("Ya existe una compañía registrada con el mismo codigo.");
            }
              
            var entity = _mapper.Map<CompanyEntity>(request);

            _companyRepository.Add(entity);
            await _companyRepository.SaveChangesAsync(ct);

            return ValidationResponse.Success("Compañía creada correctamente.");
        }



    }


}
