using Dapper;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using UserManagement.Cfg;
using UserManagement.Model;
using UserManagement.Repos.Interfaces;

namespace UserManagement.Repos
{
    public class UserRepo : MySqlRepoBase<User>, IUserRepo
    {
        public UserRepo(IOptionsSnapshot<AppConfig> cfg, ILogger<UserRepo> logger) : base(cfg, logger) { }

        public async Task<User> GetUserByCredentials(string userName, string pwd)
        {
            try
            {
                const string validateQuery = "SELECT id, userName, role FROM Users where userName = @userName and password = @password LIMIT 1";
                using var connection = new MySqlConnection(ConnectionString);
                await connection.OpenAsync();
                var user = await connection.QuerySingleOrDefaultAsync<User>(validateQuery, new { userName, password = pwd });
                return user;
            }
            catch (Exception ex)
            {
                Logger.LogError($"{nameof(GetUserByCredentials)} Failed", ex);
                throw;
            }
        }
    }
}
