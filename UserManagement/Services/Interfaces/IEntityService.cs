using UserManagement.DTO;

namespace UserManagement.Services.Interfaces
{
    public interface IEntityService<TCreateRequest, TResponseDTO> where TResponseDTO: class
    {
        Task<Response<TResponseDTO>> GetByIdAsync(Guid id);
        Task<IList<TResponseDTO>> GetAllAsync();
        Task<Response<TResponseDTO>> CreateAsync(TCreateRequest entity);
        Task<Response<TResponseDTO>> UpdateAsync(Guid id, TCreateRequest entity);
        Task<Response<TResponseDTO>> DeleteAsync(Guid id);
        //Task<BulkOperationResponse> Bulk(BulkOperationRequest id);

    }
}
