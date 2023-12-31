﻿using Dapper;
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
        protected readonly string ConnectionString;
        protected readonly ILogger<MySqlRepoBase<T>> Logger;
        private readonly string _deleteQuery;

        protected MySqlRepoBase(IOptionsSnapshot<AppConfig> cfg, ILogger<MySqlRepoBase<T>> logger)
        {
            ConnectionString = cfg.Value.ConnectionStrings.Db;
            var tableName = ExtractTableName();
            _deleteQuery = $"DELETE FROM {tableName} WHERE Id = @Id";
            Logger = logger;
        }

        public virtual async Task<T> CreateAsync(T entity)
        {
            try
            {
                await using var connection = new MySqlConnection(ConnectionString);
                await connection.OpenAsync();
                await connection.InsertAsync(entityToInsert: entity);
                var created = await connection.GetAsync<T>(entity.Id);
                return created;
            }
            catch (Exception e)
            {
                Logger.LogError("Failed to Create: {err}", e);
                throw;
            }
        }
          

        public virtual async Task<bool> UpdateAsync(T entity)
        {
            try
            {
                await using var connection = new MySqlConnection(ConnectionString);
                await connection.OpenAsync();
                var isSuccessful = await connection.UpdateAsync(entityToUpdate: entity);
                return isSuccessful;
            }
            catch (Exception e)
            {
                Logger.LogError("Failed to Update: {err}", e);
                throw;
            }
        }

        public virtual async Task<bool> DeleteByIdAsync(int id)
        {
            try
            {
                await using var connection = new MySqlConnection(ConnectionString);
                await connection.OpenAsync();
                var rowsAffected = await connection.ExecuteAsync(_deleteQuery, new { Id = id });
                var isSuccessful = (rowsAffected > 0);
                return isSuccessful;
            }
            catch (Exception e)
            {
                Logger.LogError("Failed to delete by Id: {err}", e);
                throw;
            }
        }

        public virtual async Task<IList<T>> GetAllAsync()
        {
            try 
            {
                await using var connection = new MySqlConnection(ConnectionString);
                await connection.OpenAsync();
                var results = await connection.GetAllAsync<T>();
                return results.ToList();
            }
            catch (Exception e)
            {
                Logger.LogError("Failed to get all: {err}", e);
                throw;
            }
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            try
            {
                await using var connection = new MySqlConnection(ConnectionString);
                await connection.OpenAsync();
                var result = await connection.GetAsync<T>(id);
                return result;
            }
            catch (Exception e)
            {
                Logger.LogError("Failed to get by Id : {err}", e);
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
