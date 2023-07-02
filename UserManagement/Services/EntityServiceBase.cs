using AutoMapper;
using UserManagement.DTO;
using UserManagement.Repos.Interfaces;
using UserManagement.Services.Interfaces;
using UserManagement.Utilities;


namespace UserManagement.Services
{
    public abstract class EntityServiceBase<TCreateRequestDTO, TResponseDTO, TModel> : IEntityService<TCreateRequestDTO, TResponseDTO> 
        where  TCreateRequestDTO : class, IIdEntityDto
        where TResponseDTO : class, IIdEntityDto
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

        public virtual async Task<Response<TResponseDTO>> GetByIdAsync(Guid id)
        {
            try 
            {
                var result = await Repo.GetByIdAsync(id);
                var mappedResult = Mapper.Map<TResponseDTO>(result);
                var response = Response<TResponseDTO>.From(mappedResult, $"Got {id} successfully");
                return response;
            }
            catch (Exception ex)
            {
                return Response<TResponseDTO>.From(ex, $"Failed Getting {id}: {ex.Message}");
            }
        }

        public virtual async Task<BulkOperationResponse<TResponseDTO>> GetAllAsync()
        {
            try
            {
                var results = await Repo.GetAllAsync();
                var mappedResponses = Mapper.Map<List<TResponseDTO>>(results);
                return BulkOperationResponse<TResponseDTO>.From(mappedResponses);
            }
            catch (Exception ex)
            {
                var exception = new Exception($"Failed Getting all entities: {ex.Message}");
                return BulkOperationResponse<TResponseDTO>.From(exception);
            }
        }
		
        public virtual async Task<Response<TResponseDTO>> CreateAsync(TCreateRequestDTO entity) {
            try
            {
                var mapped = Mapper.Map<TModel>(entity);
                var result = await Repo.CreateAsync(mapped);
                var mappedResult = Mapper.Map<TResponseDTO>(result);
                var response = Response<TResponseDTO>.From(mappedResult, $"Created {entity.Id} successfully");
                return response;
            }
            catch (Exception ex)
            {
                return Response<TResponseDTO>.From(ex, $"Failed Creating {entity.Id}: {ex.Message}");
            }
        }

        public virtual async Task<Response<TResponseDTO>> UpdateAsync(Guid id, TCreateRequestDTO entity)
        {
            entity.Id = id;

            return await UpdateAsync(entity);
        }

        private async Task<Response<TResponseDTO>> UpdateAsync(TCreateRequestDTO entity)
        {
            try
            {
                var mapped = Mapper.Map<TModel>(entity);
                var result = await Repo.UpdateAsync(mapped);
                return Response<TResponseDTO>.From(result, $"Updated {entity.Id} successfully");
            }
            catch (Exception ex)
            {
                return Response<TResponseDTO>.From(ex, $"Failed Updating {entity.Id}: {ex.Message}");
            }
        }

        private async Task<Response<TResponseDTO>> DeleteAsync(TCreateRequestDTO entity)
        {
            return await DeleteAsync(entity.Id);
        }

        public virtual async Task<Response<TResponseDTO>> DeleteAsync(Guid id)
        {
            try
            {
                var result = await Repo.DeleteByIdAsync(id);
                return Response<TResponseDTO>.From(result, $"Deleted {id} successfully");
            }
            catch (Exception ex)
            {
                return Response<TResponseDTO>.From(ex, $"Failed Deleting {id}: {ex.Message}"); ;
            }
        }

        public async Task<BulkOperationResponse<TResponseDTO>> Bulk(BulkOperationRequest bulkOperationRequest)
        {
            var responses = new List<Response<TResponseDTO>>();
            
            try
            {
                var api = GetMatchingApi(bulkOperationRequest.OperationType);
                var entityBatches = FileHelper.BatchReadCsv<TCreateRequestDTO>(bulkOperationRequest.InputFile);
                foreach (var batch in entityBatches)
                {
                    var tasks = batch.Select(api);
                    var results = await Task.WhenAll(tasks);
                    responses.AddRange(results);
                }
            }
            catch (Exception ex)
            {
                return BulkOperationResponse<TResponseDTO>.From(ex);
            }

            var bulkOperationResponse = BulkOperationResponse<TResponseDTO>.From(responses);
            return bulkOperationResponse;
        }


        private Func<TCreateRequestDTO, Task<Response<TResponseDTO>>> GetMatchingApi(OperationType opType)
        {
            Func<TCreateRequestDTO, Task<Response<TResponseDTO>>> operation = opType switch
            {
                OperationType.Create => CreateAsync,
                OperationType.Update => UpdateAsync,
                OperationType.Delete => DeleteAsync,
                _ => throw new InvalidOperationException($"Invalid operation {opType}")
            };

            return operation;
        }

    }
}
