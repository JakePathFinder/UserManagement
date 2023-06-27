namespace UserManagement.Repos.Interfaces
{
    public interface IEntityRepo<T>
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IList<T>> GetAllAsync();
        Task<T> CreateAsync(T entity);
        Task<bool> UpdateAsync(T Entity);
        Task<bool> DeleteByIdAsync(Guid id);
    }
}
