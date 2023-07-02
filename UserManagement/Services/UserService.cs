using AutoMapper;
using UserManagement.DTO;
using UserManagement.Model;
using UserManagement.Repos.Interfaces;
using UserManagement.Services.Interfaces;

namespace UserManagement.Services
{
    public class UserService : EntityServiceBase<DTO.CreateUserRequest, DTO.UserResponse, Model.User>
    {
        private readonly ISecurityService _securityService;

        public UserService(IEntityRepo<User> userRepo, IMapper mapper, ILogger<UserService> logger, ISecurityService securityService) : base(userRepo, mapper, logger)
        {
            _securityService = securityService;
        }

        public override async Task<Response<UserResponse>> CreateAsync(CreateUserRequest entity)
        {
            entity.Password = _securityService.HashSaltPassword(entity.Password);
            return await base.CreateAsync(entity);
        }  
    }
}
