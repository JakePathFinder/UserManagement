using AutoMapper;
using UserManagement.DTO;
using UserManagement.Model;
using UserManagement.Repos.Interfaces;
using UserManagement.Services.Interfaces;

namespace UserManagement.Services
{
    public abstract class EntityServiceBase<TCreateRequestDTO, TResponseDTO, TModel> : IEntityService<TCreateRequestDTO, TResponseDTO> where  TCreateRequestDTO : ICreateRequest
    {
        protected readonly IMapper Mapper;
        protected readonly IEntityRepo<TModel> Repo;
        protected readonly ILogger<EntityServiceBase<TCreateRequestDTO, TResponseDTO, TModel>> Logger;
        protected EntityServiceBase(IEntityRepo<TModel> entityRepo, IMapper mapper, ILogger<EntityServiceBase<TCreateRequestDTO, TResponseDTO, TModel>> logger)
        {
            Repo = entityRepo;
            Mapper = mapper;
            Logger = logger;
        }

        public virtual async Task<TResponseDTO> GetByIdAsync(Guid id)
        {
            var result = await Repo.GetByIdAsync(id);
            var mapped = Mapper.Map<TResponseDTO>(result);
            return mapped;
        }

        public virtual async Task<IList<TResponseDTO>> GetAllAsync()
        {
            var results = await Repo.GetAllAsync();
            var mapped = Mapper.Map<List<TResponseDTO>>(results);
            return mapped;
        }
		
        public virtual async Task<TResponseDTO> CreateAsync(TCreateRequestDTO entity) {
            var mapped = Mapper.Map<TModel>(entity);
            var result = await Repo.CreateAsync(mapped);
            var mappedResponse = Mapper.Map<TResponseDTO>(result);
            return mappedResponse;
        }

        public virtual async Task<bool> UpdateAsync(Guid id, TCreateRequestDTO entity)
        {
            if (entity.Id != id)
            {
                entity.Id = id;
            }

            return await UpdateAsync(entity);
        }

        public virtual async Task<bool> UpdateAsync(TCreateRequestDTO entity)
        {
            var mapped = Mapper.Map<TModel>(entity);
            var res = await Repo.UpdateAsync(mapped);

            return res;
        }

        public virtual async Task<bool> DeleteAsync(Guid id)
        {
            var res = await Repo.DeleteByIdAsync(id);
            return res;
        }

        public virtual async Task<bool> DeleteAsync(TCreateRequestDTO entity)
        {
            var res = await DeleteAsync(entity.Id);
            return res;
        }

        public Task<bool> Bulk(BulkOperationRequest bulkOperationRequest)
        {
            throw new NotImplementedException();
        }

            private void ValidateRequestType(OperationType operationType)
        {
            throw new NotImplementedException();
        }
    }
}
