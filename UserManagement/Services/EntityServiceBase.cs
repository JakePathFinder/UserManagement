using AutoMapper;
using UserManagement.Model;
using UserManagement.Repos.Interfaces;
using UserManagement.Services.Interfaces;

namespace UserManagement.Services
{
    public abstract class EntityServiceBase<TCreateRequestDTO, TResponseDTO, TModel> : IEntityService<TCreateRequestDTO, TResponseDTO>
    {
        protected readonly IMapper _mapper;
        protected readonly IEntityRepo<TModel> _repo;
        public EntityServiceBase(IEntityRepo<TModel> entityRepo, IMapper mapper)
        {
            _repo = entityRepo;
            _mapper = mapper;
        }

        public virtual async Task<TResponseDTO> GetByIdAsync(Guid id)
        {
            var result = await _repo.GetByIdAsync(id);
            var mapped = _mapper.Map<TResponseDTO>(result);
            return mapped;
        }

        public virtual async Task<IList<TResponseDTO>> GetAllAsync()
        {
            var results = await _repo.GetAllAsync();
            var mapped = _mapper.Map<List<TResponseDTO>>(results);
            return mapped;
        }

        public virtual async Task<TResponseDTO> CreateAsync(TCreateRequestDTO entity) { 
            var mapped = _mapper.Map<TModel>(entity);
            var result = await _repo.CreateAsync(mapped);
            var mappedResponse = _mapper.Map<TResponseDTO>(result);
            return mappedResponse;
        }

        public virtual async Task<bool> UpdateAsync(TCreateRequestDTO entity)
        {
            var mapped = _mapper.Map<TModel>(entity);
            var res = await _repo.UpdateAsync(mapped);
            return res;
        }

        public virtual async Task<bool> DeleteByIdAsync(Guid id)
        {
            var res = await _repo.DeleteByIdAsync(id);
            return res;
        }
    }
}
