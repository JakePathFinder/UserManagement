namespace UserManagement.Services.Interfaces
{
    public interface IEntityService<TCreateRequest, TResponseDTO>
    {
        Task<TResponseDTO> GetByIdAsync(Guid id);
        Task<IList<TResponseDTO>> GetAllAsync();
        Task<TResponseDTO> CreateAsync(TCreateRequest entity);
        Task<bool> UpdateAsync(TCreateRequest Entity);
        Task<bool> DeleteByIdAsync(Guid id);

    }
}
