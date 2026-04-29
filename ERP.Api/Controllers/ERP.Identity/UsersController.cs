
using ERP.Api.Presentation.Contracts;
using ERP.Identity.Application.DTOs.Requests;
using ERP.Identity.Application.DTOs.Responses;
using ERP.Identity.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Api.Controllers.ERP.Identity
{
    [ApiController]
    [Route("api/identity/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<UserResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> create([FromBody] CreateUserRequest request)
        {
            var data = await _service.create_user(request);
            return Ok(new ApiResponse<UserResponse>
            {
                Success = true,
                StatusCode= 200,
                Message = data.message,
                Data = null
            });
        }
    }
}
