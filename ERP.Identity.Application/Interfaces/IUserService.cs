using ERP.Identity.Application.DTOs.Requests;
using ERP.Identity.Application.DTOs.Responses;

namespace ERP.Identity.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserResponse> create_user(CreateUserRequest request);
    }
}
