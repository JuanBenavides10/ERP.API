using AutoMapper;
using ERP.Api.Presentation.Contracts.Responses;
using ERP.Identity.Application.DTOs.Requests;
using ERP.Identity.Application.DTOs.Responses;
using ERP.Identity.Application.Interfaces;
using ERP.Identity.Application.Security;
using ERP.Identity.Domain.Entities;
using ERP.Shared.Common;
using ERP.Shared.Exceptions;


namespace ERP.Identity.Application.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _repository;
        private readonly JwtHelper _jwtHelper;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repository, IMapper mapper, JwtHelper jwtHelper)
        {
            _repository = repository;
            _mapper = mapper;
            _jwtHelper = jwtHelper;
        }

        public async Task<ValidationResult> create_user(CreateUserRequest request)
        {

            var exists = await _repository.user_name_exists(request.UserName);

            if (exists) return ValidationResult.Failure($"El nombre de usuario {request.UserName} ya existe");

            byte[] hash, salt;
            PasswordHelper.create_password_hash(request.Password, out hash, out salt);

            var person = _mapper.Map<PersonEntity>(request); 
            person.Active = true;

            var user = new UsersEntity
            {
                UserName = request.UserName,
                PasswordHash = hash,
                PasswordSalt = salt,
                Active = true,
            };

            var result = await _repository.create_user(person, user);
            
            return ValidationResult.Success("registrado correctamente");
        }

        public async Task<LoginResponse?> login(LoginRequest request)
        {
            var user = await _repository.get_user_by_user_name(request.UserName);

            if (user == null) return null;

            bool isValid = PasswordHelper.verify_password(request.Password, user.PasswordHash, user.PasswordSalt);

            if(!isValid) return null;

            var token = _jwtHelper.generate_token(user);

            return new LoginResponse
            {
                Token = token,
                UserName = user.UserName,
                PersonId = user.PersonId.ToString(),
                FirstName = user.Person.FirstName,
                LastName = user.Person.LastName,
            };
        }
    }
}
