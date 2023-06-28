﻿using AutoMapper;
using UserManagement.Repos.Interfaces;
using UserManagement.Services.Interfaces;

namespace UserManagement.Services
{
    public class UserService : EntityServiceBase<DTO.CreateUserRequest, DTO.UserResponse, Model.User>
    {
        private readonly ISecurityService _securityService;

        public UserService(IUserRepo userRepo, IMapper mapper, ILogger<UserService> logger, ISecurityService securityService) : base(userRepo, mapper, logger)
        {
            _securityService = securityService;
        }
    }
}
