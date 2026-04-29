using ERP.Api.Presentation.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Api.Presentation.Factories
{
    public static class ApiResponseFactory
    {
        //Factory ->  entrega el objeto final ya armado para usarlo directamente.
        public static IActionResult ValidationError(string message)
        {
            var response = new ApiResponse<object>
            {
                Success = false,
                StatusCode = StatusCodes.Status422UnprocessableEntity,
                Message = "Error de validación",
                Errors = new Dictionary<string, string[]>
                {
                    {  "Detail", new[] { message } }
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
                Errors = new Dictionary<string, string[]>
                {
                    { "Detail", new[] {  ex.Message } }
                }
            };
            return new ObjectResult(response)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }

        //Aplicamos Overload = mismo nombre, diferentes parámetros.
        public static IActionResult Success<T>(T? data, string message = "Operación exitosa") //acepta data
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
        public static IActionResult Success(string message = "Operación exitosa") //no acepta data, solo mensaje
        {
            var response = new ApiResponse<object?>
            {
                Success = true,
                StatusCode= StatusCodes.Status200OK,
                Message = message,
                Data = null
            };

            return new ObjectResult(response) { StatusCode = StatusCodes.Status200OK };
        }

    }
}
