namespace UserManagement.Services.Interfaces
{
    public interface IEntityService<TCreateRequest, TResponseDTO>
    {
        Task<TResponseDTO> GetByIdAsync(Guid id);
        Task<TResponseDTO> CreateAsync(TCreateRequest entity);
        Task<bool> UpdateAsync(Guid id, TCreateRequest entity);
        Task<bool> DeleteByIdAsync(Guid id);

    }
}
