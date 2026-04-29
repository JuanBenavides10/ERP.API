using ERP.Accounting.Application.DTOs.Requests.Configuration;
using ERP.Accounting.Application.Interfaces;
using ERP.Accounting.Application.Interfaces.Configuration;
using ERP.Accounting.Application.Services;
using ERP.Accounting.Application.Services.Configuration;
using ERP.Api.Presentation.Contracts.Responses;
using ERP.Api.Presentation.Factories;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Api.Controllers.ERP.Accounting.Configuration
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
        public async Task<IActionResult> Create([FromBody] CreateCompanyRequest request, CancellationToken ct)
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
        public async Task<IActionResult> Update(Guid uuid, [FromBody] UpdateCompanyRequest request, CancellationToken ct)
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
