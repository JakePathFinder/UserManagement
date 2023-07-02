using UserManagement.DTO;

namespace UserManagement.Services.Interfaces
{
    public interface IEntityService<TCreateRequest, TResponseDTO> where TResponseDTO: class, IIdEntityDto
    {
        Task<Response<TResponseDTO>> GetByIdAsync(int id);
        Task<BulkOperationResponse<TResponseDTO>> GetAllAsync();
        Task<Response<TResponseDTO>> CreateAsync(TCreateRequest entity);
        Task<Response<TResponseDTO>> UpdateAsync(int id, TCreateRequest entity);
        Task<Response<TResponseDTO>> DeleteAsync(int id);
        Task<BulkOperationResponse<TResponseDTO>> Bulk(OperationType operationType, IFormFile file);

    }
}
