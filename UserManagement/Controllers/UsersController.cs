using UserManagement.DTO;
using UserManagement.Services.Interfaces;

namespace UserManagement.Controllers;

public class UsersController : EntityBaseController<CreateUserRequest, UserResponse>
{

    public UsersController(ILogger<UsersController> logger, IEntityService<CreateUserRequest, UserResponse> service) : base(logger:logger, service: service)
    {
    }

}
