using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System.Reflection;
using UserManagement.Cfg;
using UserManagement.Model;
using UserManagement.Repos.Interfaces;


namespace UserManagement.Repos
{
    public abstract class MySqlRepoBase<T> : IEntityRepo<T> where T: class, IEntity
    {
        protected readonly string _connectionString;
        protected readonly ILogger<MySqlRepoBase<T>> _logger;
        private readonly string _tableName;
        private readonly string _deleteQuery;

        protected MySqlRepoBase(IOptionsSnapshot<AppConfig> cfg, ILogger<MySqlRepoBase<T>> logger)
        {
            _connectionString = cfg.Value.ConnectionStrings.Db;
            _tableName = MySqlRepoBase<T>.ExtractTableName();
            _deleteQuery = $"DELETE FROM {_tableName} WHERE Id = @Id";
            _logger = logger;
        }

        public virtual async Task<T> CreateAsync(T entity)
        {
            try
            {
                await using var connection = new MySqlConnection(_connectionString);
                await connection.OpenAsync();
                await connection.InsertAsync(entityToInsert: entity);
                var created = await connection.GetAsync<T>(entity.Id);
                return created;
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to Create",ex);
                throw;
            }
        }
          

        public virtual async Task<bool> UpdateAsync(T entity)
        {
            try
            {
                await using var connection = new MySqlConnection(_connectionString);
                await connection.OpenAsync();
                var isSuccessful = await connection.UpdateAsync(entityToUpdate: entity);
                return isSuccessful;
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to Update", ex);
                throw;
            }
        }

        public virtual async Task<bool> DeleteByIdAsync(Guid id)
        {
            try
            {
                await using var connection = new MySqlConnection(_connectionString);
                await connection.OpenAsync();
                var rowsAffected = await connection.ExecuteAsync(_deleteQuery, new { Id = id });
                var isSuccessful = (rowsAffected > 0);
                return isSuccessful;
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to delete by Id",ex);
                throw;
            }
        }

        public virtual async Task<IList<T>> GetAllAsync()
        {
            try 
            {
                await using var connection = new MySqlConnection(_connectionString);
                await connection.OpenAsync();
                var results = await connection.GetAllAsync<T>();
                return results.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get all",ex);
                throw;
            }
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            try
            {
                await using var connection = new MySqlConnection(_connectionString);
                await connection.OpenAsync();
                var result = await connection.GetAsync<T>(id);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get by Id",ex);
                throw;
            }
        }

        private static string ExtractTableName()
        {
            var tableName = typeof(T).GetCustomAttribute<TableAttribute>()?.Name;
            if (string.IsNullOrEmpty(tableName))
            {
                throw new InvalidOperationException("Table name not specified.");
            }
            return tableName;
        }
    }
}
