using ERP.Api.Presentation.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Api.Presentation.Factories
{
    public static class ApiResponseFactory
    {
        public static IActionResult ValidationError(string message)
        {
            var response = new ApiResponse<object>
            {
                success = false,
                status_code = StatusCodes.Status422UnprocessableEntity,
                message = "Error de validación",
                errors = new Dictionary<string, string>
                {
                    {  "Detail", message }
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
                success = false,
                status_code = StatusCodes.Status500InternalServerError,
                message = "Error interno del servidor",
                errors = new Dictionary<string, string>
                {
                    { "Detail", ex.Message }
                }
            };
            return new ObjectResult(response)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }

        //Aplicamos Overload = mismo nombre, diferentes parámetros.
        public static IActionResult Success<T>(T? data, string message = "Operación exitosa")
        {
            var response = new ApiResponse<T>
            {
                success = true,
                status_code = StatusCodes.Status200OK,
                message = message,
                data = data
            };

            return new ObjectResult(response)
            {
                StatusCode = StatusCodes.Status200OK
            };
        }
        public static IActionResult Success(string message = "Operación exitosa")
        {
            var response = new ApiResponse<object?>
            {
                success = true,
                status_code = StatusCodes.Status200OK,
                message = message,
                data = null
            };

            return new ObjectResult(response) { StatusCode = StatusCodes.Status200OK };
        }

    }
}
