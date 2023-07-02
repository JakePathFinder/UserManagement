using System.ComponentModel.DataAnnotations;
using UserManagement.Const;

namespace UserManagement.DTO
{
    public interface IIdEntityDto
    {
        Guid Id { get; set; }
    }
}
