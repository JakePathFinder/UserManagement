using UserManagement.DTO;
using UserManagement.Services.Interfaces;

namespace UserManagement.Controllers;

public class UserController : EntityBaseController<CreateUserRequest, UserResponse>
{

    public UserController(ILogger<UserController> logger, IEntityService<CreateUserRequest, UserResponse> service) : base(logger:logger, service: service)
    {
    }

}
