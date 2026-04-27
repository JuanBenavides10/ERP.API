using ERP.Api.Presentation.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Api.Presentation.Factories
{
    public static class ApiResponseFactory
    {
        public static IActionResult ValidationError(string field, List<string> messages)
        {
            var response = new ApiResponse<object>
            {
                Success = false,
                StatusCode = StatusCodes.Status422UnprocessableEntity,
                Message = "Error de validación",
                Errors = new Dictionary<string, List<string>>
                {
                    { field, messages }
                }
            };

            return new ObjectResult(response)
            {
                StatusCode = StatusCodes.Status422UnprocessableEntity
            };
        }

        public static IActionResult ServerError(Exception ex)
        {
            var response = new ApiResponse<object>
            {
                Success = false,
                StatusCode = StatusCodes.Status500InternalServerError,
                Message = "Error interno del servidor",
                Errors = new Dictionary<string, List<string>>
                {
                    { "Detail", new List<string> { ex.Message } }
                }
            };
            return new ObjectResult(response)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }

        public static IActionResult Success<T>(T data, string message = "Operación exitosa")
        {
            var response = new ApiResponse<T>
            {
                Success = true,
                StatusCode = StatusCodes.Status200OK,
                Message = message,
                Data = data
            };

            return new ObjectResult(response)
            {
                StatusCode = StatusCodes.Status200OK
            };

        }
    }
}
