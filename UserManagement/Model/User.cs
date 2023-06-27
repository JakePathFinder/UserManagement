using Dapper.Contrib.Extensions;

namespace UserManagement.Model
{
    [Table("Users")]
    public class User : IEntity
    {
        [ExplicitKey]
        public Guid Id { get; init; }
        public required string FirstName { get; init; }
        public required string LastName { get; init; }
        public required string PhoneNumber { get; init; }
        public required string Email { get; init; }
        public required string Password { get; init; }
    }
}
