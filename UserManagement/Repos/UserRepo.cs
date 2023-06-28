using Dapper;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using UserManagement.Cfg;
using UserManagement.Model;
using UserManagement.Repos.Interfaces;

namespace UserManagement.Repos
{
    public class UserRepo : MySqlRepoBase<User>
    {
        public UserRepo(IOptionsSnapshot<AppConfig> cfg, ILogger<UserRepo> logger) : base(cfg, logger) { }
    }
}
