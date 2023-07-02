using UserManagement.DTO;

namespace UserManagement.Services.Interfaces
{
    public interface IEntityService<TCreateRequest, TResponseDTO>
    {
        Task<TResponseDTO> GetByIdAsync(Guid id);
        Task<IList<TResponseDTO>> GetAllAsync();
        Task<TResponseDTO> CreateAsync(TCreateRequest entity);
        Task<bool> UpdateAsync(Guid id, TCreateRequest entity);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> Bulk(BulkOperationRequest bulkOperationRequest);
    }
}
