using AutoMapper;
using ERP.Shared.Common;
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
        private const int MaxLogoBytes = 1 * 1024 * 1024; // 1 M
        public CompanyService(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        public async Task<ValidationResult<GetCompanyResponse>> GetByIdAsync(Guid uuid, CancellationToken ct)
        {
            if (uuid == Guid.Empty)
            {
                return ValidationResult<GetCompanyResponse>.Failure("El identificador de la compañía es inválido.");
            }
            var entity = await _companyRepository.GetByIdAsync(uuid, ct);

            if (entity == null)
            {
                return ValidationResult<GetCompanyResponse>.Failure("La compañía consultada no se encuentra registrada en el sistema.");
            }

            var dto = _mapper.Map<GetCompanyResponse>(entity);

            return ValidationResult<GetCompanyResponse>.Success(dto, "Compañía obtenida correctamente");
        }

        public async Task<ValidationResult> CreateAsync(CreateCompanyRequest request, CancellationToken ct)
        {      
            var exists = await _companyRepository.ExistsByCodeAsync(request.Code, ct);

            if (exists)
            {
                return ValidationResult.Failure("Ya existe una compañía registrada con el mismo codigo.");
            }

            var logoValidation = ValidateLogoBytes(request.Logo);
            if (!logoValidation.IsValid)
            {
                return logoValidation;
            }
             
            var entity = _mapper.Map<CompanyEntity>(request);

            _companyRepository.Add(entity);
            await _companyRepository.SaveChangesAsync(ct);

            return ValidationResult.Success("Compañía creada correctamente.");
        }

        public async Task<ValidationResult> UpdateAsync(Guid uuid, UpdateCompanyRequest request, CancellationToken ct)
        {
            if (uuid == Guid.Empty)
            {
                return ValidationResult.Failure("El identificador de la compañía es inválido.");
            }        
            // Importante: para actualizar necesitas entidad TRACKED
            var entity = await _companyRepository.GetByIdForUpdateAsync(uuid, ct);

            if (entity is null)
            {
                return ValidationResult.Failure("La compañía consultada no se encuentra registrada en el sistema.");
            }

            var logoValidation = ValidateLogoBytes(request.Logo);
            if (!logoValidation.IsValid)
            {
                return logoValidation;
            }

            // Mapear request -> entity existente (tracked)
            _mapper.Map(request, entity);

            await _companyRepository.SaveChangesAsync(ct);

            return ValidationResult.Success("Compañía actualizada correctamente.");
        }

        private static ValidationResult ValidateLogoBytes(byte[]? logo)
        {
            if (logo is null)
            {
                return ValidationResult.Success("OK"); // no envió logo
            }
              
            if (logo.Length == 0)
            {
                return ValidationResult.Failure("El logo está vacío.");
            }
              
            if (logo.Length > MaxLogoBytes)
            {
                return ValidationResult.Failure("El logo supera el tamaño máximo permitido (1 MB).");
            }
               
            return ValidationResult.Success("OK");
        }


    }


}
