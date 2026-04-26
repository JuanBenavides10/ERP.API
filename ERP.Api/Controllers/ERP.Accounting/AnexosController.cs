using ERP.Accounting.Application.DTOs.Requests;
using ERP.Accounting.Application.Interfaces;
using ERP.Api.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Api.Controllers.ERP.Accounting
{

    [ApiController]
    [Route("api/accounting/anexos")]
    public class AnexosController : ControllerBase
    {

        private readonly IAnexosService _service;

        public AnexosController(IAnexosService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();

            return Ok(new ApiResponse<List<object>>
            {
                Success = true,
                StatusCode = 200,
                Message = "OK",
                Data = data.Cast<object>().ToList()
            });
        }
     
        [HttpGet("{uuid:guid}")]
        public async Task<IActionResult> GetByUuid(Guid uuid)
        {       
            var data = await _service.GetByUuidAsync(uuid);
            if (data is null)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    StatusCode = 404,
                    Message = "Anexo no encontrado"
                });
            }

            return Ok(new ApiResponse<object>
            {
                Success = true,
                StatusCode = 200,
                Message = "OK",
                Data = data
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAnexosRequest request)
        {
            var data = await _service.CreateAsync(request);

            return CreatedAtAction(nameof(GetByUuid), new { uuid = data.UuId }, new ApiResponse<object>
            {
                Success = true,
                StatusCode = 201,
                Message = "Creado",
                Data = data
            });
        }

        [HttpPut("{uuid:guid}")]
        public async Task<IActionResult> Update(Guid uuid, [FromBody] UpdateAnexosRequest request)
        {
            var ok = await _service.UpdateAsync(uuid, request);
            if (!ok)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    StatusCode = 404,
                    Message = "Anexo no encontrado"
                });
            }

            return Ok(new ApiResponse<object>
            {
                Success = true,
                StatusCode = 200,
                Message = "Actualizado"
            });
        }

        [HttpDelete("{uuid:guid}")]
        public async Task<IActionResult> Delete(Guid uuid)
        {
            var ok = await _service.DeleteAsync(uuid);
            if (!ok)
            {
                return NotFound(new ApiResponse<object>{Success = false,StatusCode = 404,Message = "Anexo no encontrado"});
            }

            return Ok(new ApiResponse<object>
            {
                Success = true,
                StatusCode = 200,
                Message = "Eliminado" 
            });
        }

    }
}
