using AutoMapper;
using UserManagement.Model;
using UserManagement.Repos.Interfaces;
using UserManagement.Services.Interfaces;

namespace UserManagement.Services
{
    public abstract class EntityServiceBase<TCreateRequestDTO, TResponseDTO, TModel> : IEntityService<TCreateRequestDTO, TResponseDTO>
    {
        protected readonly IMapper Mapper;
        protected readonly IEntityRepo<TModel> Repo;
        private readonly ILogger<EntityServiceBase<TCreateRequestDTO, TResponseDTO, TModel>> _logger;
        protected EntityServiceBase(IEntityRepo<TModel> entityRepo, IMapper mapper, ILogger<EntityServiceBase<TCreateRequestDTO, TResponseDTO, TModel>> logger)
        {
            Repo = entityRepo;
            Mapper = mapper;
            _logger = logger;
        }

        public virtual async Task<TResponseDTO> GetByIdAsync(Guid id)
        {
            _logger.LogInformation("Creating {entityType} By Id: {id}", typeof(TResponseDTO).Name,id);
            var result = await Repo.GetByIdAsync(id);
            var mapped = Mapper.Map<TResponseDTO>(result);
            return mapped;
        }

        public virtual async Task<IList<TResponseDTO>> GetAllAsync()
        {
            _logger.LogInformation("Getting all {entityType} records", typeof(TResponseDTO).Name);
            var results = await Repo.GetAllAsync();
            var mapped = Mapper.Map<List<TResponseDTO>>(results);
            return mapped;
        }

        public virtual async Task<TResponseDTO> CreateAsync(TCreateRequestDTO entity) {
            _logger.LogInformation("Creating a new {entityType}", typeof(TResponseDTO).Name);
            var mapped = Mapper.Map<TModel>(entity);
            var result = await Repo.CreateAsync(mapped);
            var mappedResponse = Mapper.Map<TResponseDTO>(result);
            return mappedResponse;
        }

        public virtual async Task<bool> UpdateAsync(TCreateRequestDTO entity)
        {
            _logger.LogInformation("Updating {entityType}", typeof(TResponseDTO).Name);
            var mapped = Mapper.Map<TModel>(entity);
            var res = await Repo.UpdateAsync(mapped);
            return res;
        }

        public virtual async Task<bool> DeleteByIdAsync(Guid id)
        {
            _logger.LogInformation("Deleting {entityType} with Id {id}", typeof(TResponseDTO).Name, id);
            var res = await Repo.DeleteByIdAsync(id);
            return res;
        }
    }
}
