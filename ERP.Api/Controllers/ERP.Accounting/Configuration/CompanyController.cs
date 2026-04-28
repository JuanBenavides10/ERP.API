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

                if (result.is_valid)
                {
                    return ApiResponseFactory.ValidationError(result.message);
                }

                return ApiResponseFactory.Success(result.data, result.message);
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

                if (result.is_valid)
                {
                    return ApiResponseFactory.ValidationError(result.message);
                }

                return ApiResponseFactory.Success(result.message);
            }
            catch (Exception ex)
            {
                return ApiResponseFactory.ServerError(ex);
            }
        }

    }
}
