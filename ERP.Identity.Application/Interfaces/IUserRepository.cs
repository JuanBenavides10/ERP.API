using ERP.Identity.Application.DTOs.Requests;
using ERP.Identity.Application.DTOs.Responses;
using ERP.Identity.Domain.Entities;

namespace ERP.Identity.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<(PersonEntity person, UsersEntity user)> create_user(PersonEntity person, UsersEntity user);
        Task<bool> user_name_exists(string user_name);
        Task<UsersEntity?> get_user_by_user_name(string user_name);
    }
}
