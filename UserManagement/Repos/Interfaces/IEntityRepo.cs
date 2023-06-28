namespace UserManagement.Repos.Interfaces
{
    public interface IEntityRepo<T>
    {
        Task<T> GetByIdAsync(Guid id);
        Task<T> CreateAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteByIdAsync(Guid id);
    }
}
