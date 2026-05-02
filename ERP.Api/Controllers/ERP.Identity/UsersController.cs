
using ERP.Api.Presentation.Contracts;
using ERP.Api.Presentation.Factories;
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

        [HttpPost] //subdiarios 
        [ProducesResponseType(typeof(ApiResponse<UserResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> create([FromBody] CreateUserRequest request, CancellationToken ct) //async
        {
            try
            {
                var result = await _service.create_user(request);

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

        [HttpPost("login")]
        [ProducesResponseType(typeof(ApiResponse<UserResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> login(LoginRequest request)
        {
            var data = await _service.login(request);

            return Ok(new ApiResponse<LoginResponse>
            {
                Success = true,
                StatusCode = 200,
                Message = "OK",
                Data = data
            });
        }
    }
}
