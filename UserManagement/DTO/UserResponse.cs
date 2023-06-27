namespace UserManagement.DTO
{
    public class UserResponse
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Position { get; set; }
        public required string PhoneNumber { get; set; }
        public required string UserName { get; set; }
    }
}
