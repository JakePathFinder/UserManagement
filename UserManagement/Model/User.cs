using Dapper.Contrib.Extensions;

namespace UserManagement.Model
{
    [Table("Users")]
    public class User : IEntity
    {
        [ExplicitKey]
        public Guid Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
