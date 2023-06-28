using UserManagement.Services.Interfaces;

namespace UserManagement.Services
{
    using BCrypt.Net;
    public class SecurityService : ISecurityService
    {
        private readonly ILogger<SecurityService> _logger;
        public SecurityService(ILogger<SecurityService> logger)
        {
            _logger = logger;
        }

        public string HashSaltPassword(string password)
        {
            var hashed = BCrypt.HashPassword(password);
            return hashed;
        }

        public bool VerifyPassword(string actual, string expected)
        {
            return BCrypt.Verify(actual, expected);
        }
    }
}
