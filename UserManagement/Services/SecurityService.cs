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
            _logger.LogDebug("Hashing & Salting password");
            var hashed = BCrypt.HashPassword(password);
            return hashed;
        }

        public bool VerifyPassword(string actual, string expected)
        {
            _logger.LogDebug("verifying password");
            return BCrypt.Verify(actual, expected);
        }
    }
}
