using AutoMapper;
using UserManagement.Repos.Interfaces;

namespace UserManagement.Services
{
    public class UserService : EntityServiceBase<DTO.CreateUserRequest, DTO.UserResponse, Model.User>
    {
        public UserService(IUserRepo userRepo, IMapper mapper): base(userRepo, mapper)
        {
        }
    }
}
