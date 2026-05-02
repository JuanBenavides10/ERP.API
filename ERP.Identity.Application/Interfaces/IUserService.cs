using ERP.Api.Presentation.Contracts.Responses;
using ERP.Identity.Application.DTOs.Requests;
using ERP.Identity.Application.DTOs.Responses;
using ERP.Shared.Common;

namespace ERP.Identity.Application.Interfaces
{
    public interface IUserService
    {

        Task<ValidationResult> create_user(CreateUserRequest request);
        Task<LoginResponse?> login(LoginRequest request);
    }
}
