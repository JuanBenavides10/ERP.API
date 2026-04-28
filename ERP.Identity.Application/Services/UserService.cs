using ERP.Identity.Application.DTOs.Requests;
using ERP.Identity.Application.DTOs.Responses;
using ERP.Identity.Application.Interfaces;
using ERP.Identity.Application.Security;
using ERP.Identity.Domain.Entities;
using ERP.Shared.Exceptions;

namespace ERP.Identity.Application.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserResponse> create_user(CreateUserRequest request)
        {

            var exists = await _repository.user_name_exists(request.user_name);

            if (exists) throw new BusinessException($"El nombre de usuario {request.user_name} ya existe");

            byte[] hash, salt;
            PasswordHelper.create_password_hash(request.password, out hash, out salt);

            var person = new Person
            {
                first_name = request.first_name,
                last_name = request.last_Name,
                document_type = request.document_type,
                email = request.email,
                birth_date = request.birth_date,
                active = true,
            };

            var user = new Users
            {
                user_name = request.user_name,
                password_hash = hash,
                password_salt = salt,
                active = true,
            };

            var result = await _repository.create_user(person, user);

            return new UserResponse
            {
                id = result.user.id,
                user_name = result.user.user_name
            };
        }
    }
}
