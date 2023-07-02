using AutoMapper;
using MySqlX.XDevAPI.Common;
using UserManagement.Const;
using UserManagement.DTO;
using UserManagement.Model;
using UserManagement.Repos.Interfaces;
using UserManagement.Services.Interfaces;
using static Dapper.SqlMapper;

namespace UserManagement.Services
{
    public abstract class EntityServiceBase<TCreateRequestDTO, TResponseDTO, TModel> : IEntityService<TCreateRequestDTO, TResponseDTO> 
        where  TCreateRequestDTO : class, IIdEntityDto
        where TResponseDTO : class, IIdEntityDto
    {
        protected readonly IMapper Mapper;
        protected readonly IEntityRepo<TModel> Repo;
        protected readonly ILogger<EntityServiceBase<TCreateRequestDTO, TResponseDTO, TModel>> Logger;
        private const int BatchSize = ServiceConstants.BulkOperationsBatchSize;
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

        public virtual async Task<IList<TResponseDTO>> GetAllAsync()
        {
            try
            {
                var results = await Repo.GetAllAsync();
                var mapped = Mapper.Map<List<TResponseDTO>>(results);
                return mapped;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed Getting all entities: {ex.Message}");
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

        public async Task<BulkOperationResponse> Bulk(BulkOperationRequest bulkOperationRequest, string inputFile)
        {
            var entities = ReadEntitiesFromFile(inputFile);

            var responses = await BulkDo(bulkOperationRequest.OperationType, entities);
            return BulkOperationResponse.From(responses);
        }

        

        private async Task<List<Response<TResponseDTO>>> BulkDo(OperationType opType, List<TCreateRequestDTO> entities)
        {
            var response = new List<Response<TResponseDTO>>();

            var api = GetMatchingApi(opType);

            var batches = Batch(entities);

            foreach (var batch in batches)
            {
                var tasks = batch.Select(api);
                var results = await Task.WhenAll(tasks);
                response.AddRange(results);
            }

            return response;
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

        private static List<List<TCreateRequestDTO>> Batch(List<TCreateRequestDTO> source, int batchSize = ServiceConstants.BulkOperationsBatchSize)
        {
            var batches = new List<List<TCreateRequestDTO>>();

            for (var i = 0; i < source.Count; i += batchSize)
            {
                batches.Add(source.Skip(i).Take(batchSize).ToList());
            }

            return batches;
        }

        private static List<TCreateRequestDTO> ReadEntitiesFromFile(string inputFile)
        {
            throw new NotImplementedException();
        }
    }
}
