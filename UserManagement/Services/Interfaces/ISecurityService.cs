namespace UserManagement.Services.Interfaces
{
    public interface ISecurityService
    {
        string HashSaltPassword(string password);
        bool VerifyPassword(string actual, string expected);
    }
}
