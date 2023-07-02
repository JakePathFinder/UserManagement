using UserManagement.DTO;

namespace UserManagement.Services.Interfaces
{
    public interface IEntityService<TCreateRequest, TResponseDTO> where TResponseDTO: class, IIdEntityDto
    {
        Task<Response<TResponseDTO>> GetByIdAsync(Guid id);
        Task<BulkOperationResponse<TResponseDTO>> GetAllAsync();
        Task<Response<TResponseDTO>> CreateAsync(TCreateRequest entity);
        Task<Response<TResponseDTO>> UpdateAsync(Guid id, TCreateRequest entity);
        Task<Response<TResponseDTO>> DeleteAsync(Guid id);
        Task<BulkOperationResponse<TResponseDTO>> Bulk(OperationType operationType, IFormFile file);

    }
}
