using ERP.Api.Presentation.Contracts.Responses;
using ERP.Identity.Application.DTOs.Requests;
using ERP.Identity.Application.DTOs.Responses;

namespace ERP.Identity.Application.Interfaces
{
    public interface IUserService
    {
        //Task<UserResponse> create_user(CreateUserRequest request);
        Task<ValidationResponse> create_user(CreateUserRequest request);
    }
}
