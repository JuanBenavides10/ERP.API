using ERP.Accounting.Application.DTOs.Requests.Company;
using ERP.Accounting.Application.DTOs.Requests.Pagination;
using ERP.Accounting.Application.DTOs.Responses.Company;
using ERP.Accounting.Application.DTOs.Responses.Pagination;
using ERP.Accounting.Application.Interfaces;
using ERP.Accounting.Application.Interfaces.Company;
using ERP.Accounting.Application.Services;
using ERP.Api.Presentation.Contracts;
using ERP.Api.Presentation.Contracts.Responses;
using ERP.Api.Presentation.Factories;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Api.Controllers.ERP.Accounting.Company
{
    [ApiController]
    [Route("api/accounting/companies")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }


        [HttpGet("companies-paginadas")]

        public async Task<IActionResult> GetPaged([FromQuery] PaginacionRequest paginacion,[FromQuery] CompanyFiltrosRequest filtros,CancellationToken ct)
        {
            try
            {
                var result = await _companyService.GetPagedAsync(filtros, paginacion, ct);
             
                return ApiResponseFactory.Success(result.Data, result.Message);
            }
            catch (Exception ex)
            {
                return ApiResponseFactory.ServerError(ex);
            }
        }


        [HttpGet("{uuid:guid}")]
        public async Task<IActionResult> GetById(Guid uuid, CancellationToken ct)
        {
            try
            {
                var result = await _companyService.GetByIdAsync(uuid,ct);

                if (result.IsValid == false)
                {
                    return ApiResponseFactory.ValidationError(result.Message);
                }

                return ApiResponseFactory.Success(result.Data, result.Message);
            }
            catch (Exception ex)
            {
                return ApiResponseFactory.ServerError(ex);
            }
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [RequestSizeLimit(2 * 1024 * 1024)] // 2MB total cuerpo HTTP completo (archivo + campos)
        [RequestFormLimits(MultipartBodyLengthLimit = 2 * 1024 * 1024)] //2MB , el que convierte multipart en DTO [FromForm]

        public async Task<IActionResult> Create([FromForm] CreateCompanyRequest request, CancellationToken ct)
        {
            try
            {
                var result = await _companyService.CreateAsync(request, ct);

                if (!result.IsValid)
                {
                    return ApiResponseFactory.ValidationError(result.Message);
                }

                return ApiResponseFactory.Success(result.Message);
            }
            catch (Exception ex)
            {
                return ApiResponseFactory.ServerError(ex);
            }
        }

        [HttpPut("{uuid:guid}")]
        [Consumes("multipart/form-data")]
        [RequestSizeLimit(2 * 1024 * 1024)] // 2MB total cuerpo HTTP completo (archivo + campos)
        [RequestFormLimits(MultipartBodyLengthLimit = 2 * 1024 * 1024)] //2MB , el que convierte multipart en DTO [FromForm]
        public async Task<IActionResult> Update(Guid uuid, [FromForm] UpdateCompanyRequest request, CancellationToken ct)
        {
            try
            {
                var result = await _companyService.UpdateAsync(uuid, request, ct);

                if (!result.IsValid)
                {
                    return ApiResponseFactory.ValidationError(result.Message);
                }
                return ApiResponseFactory.Success(result.Message);
            }
            catch (Exception ex)
            {
                return ApiResponseFactory.ServerError(ex);
            }
        }

    }
}
