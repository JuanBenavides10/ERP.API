using AutoMapper;
using ERP.Accounting.Application.DTOs.Requests;
using ERP.Accounting.Application.DTOs.Requests.Company;
using ERP.Accounting.Application.DTOs.Requests.Pagination;
using ERP.Accounting.Application.DTOs.Responses;
using ERP.Accounting.Application.DTOs.Responses.Company;
using ERP.Accounting.Application.DTOs.Responses.Pagination;
using ERP.Accounting.Application.Interfaces;
using ERP.Accounting.Application.Interfaces.Company;
using ERP.Accounting.Domain.Entities;
using ERP.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.Accounting.Application.Interfaces.Storage;
using ERP.Accounting.Domain.Entities.Company;

namespace ERP.Accounting.Application.Services.Company
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        private readonly IFileStorage _fileStorage;
        public CompanyService(ICompanyRepository companyRepository, IMapper mapper , IFileStorage fileStorage)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
            _fileStorage = fileStorage;
        }
        public async Task<ValidationResult<PaginacionResponse<GetAllCompanyResponse>>> GetPagedAsync(CompanyFiltrosRequest filtros,PaginacionRequest paginacion,CancellationToken ct)
        {
            var (total, items) = await _companyRepository.GetPagedEntitiesAsync(filtros, paginacion, ct);

            var dataMapped = _mapper.Map<List<GetAllCompanyResponse>>(items);

            var response = new PaginacionResponse<GetAllCompanyResponse>
            {
                TotalRegistros = total,
                TotalPaginas = (int)Math.Ceiling(total / (double)paginacion.RecordsPorPagina),
                PaginaActual = paginacion.Pagina,
                Data = dataMapped
            };

            return ValidationResult<PaginacionResponse<GetAllCompanyResponse>>.Success(response,"Lista de compañías obtenida correctamente.");
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
  
            var entity = _mapper.Map<CompanyEntity>(request);

            //Guardar archivo local si viene
            if (request.Logo is not null)
            {
                const long maxBytes = 1 * 1024 * 1024; // 1MB
                if (request.Logo.Length > maxBytes)
                {
                    return ValidationResult.Failure("El logo no debe superar 1MB.");
                }

                entity.LogoPath = await _fileStorage.SaveAsync(request.Logo, folder: "company-logos", ct);
            }

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

            // Mapear request -> entity existente (tracked)
            _mapper.Map(request, entity);

            //Si llega un nuevo logo, borra el anterior y guarda el nuevo
            if (request.Logo is not null)
            {
                const long maxBytes = 1 * 1024 * 1024; // 1MB
                if (request.Logo.Length > maxBytes)
                {
                    return ValidationResult.Failure("El logo no debe superar 1MB.");
                }

                await _fileStorage.DeleteAsync(entity.LogoPath, ct);
                entity.LogoPath = await _fileStorage.SaveAsync(request.Logo, folder: "company-logos", ct);
            }

            await _companyRepository.SaveChangesAsync(ct);

            return ValidationResult.Success("Compañía actualizada correctamente.");
        }

       

    }
}
