namespace UserManagement.DTO
{
    public class UserResponse : IIdEntityDto
    {
        public required int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Email { get; set; }
    }
}
