using UserManagement.Model;

namespace UserManagement.Repos.Interfaces
{
    public interface IUserRepo : IEntityRepo<User>
    {
        public Task<User> GetUserByCredentials(string userName, string pwd);
    }
}
