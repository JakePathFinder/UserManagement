using System.ComponentModel.DataAnnotations;
using UserManagement.Const;

namespace UserManagement.DTO
{
    public interface ICreateRequest
    {
        Guid Id { get; set; }
    }
}
