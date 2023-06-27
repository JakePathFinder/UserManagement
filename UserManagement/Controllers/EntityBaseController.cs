using Microsoft.AspNetCore.Mvc;
using UserManagement.Services.Interfaces;

namespace UserManagement.Controllers;

[ApiController]
[Route("[controller]")]
public class EntityBaseController<TCreateRequestDTO, TResponseDTO> : ControllerBase
{
    private readonly ILogger<EntityBaseController<TCreateRequestDTO, TResponseDTO>> _logger;
    protected readonly IEntityService<TCreateRequestDTO, TResponseDTO> _service;

    public EntityBaseController(ILogger<EntityBaseController<TCreateRequestDTO, TResponseDTO>> logger, IEntityService<TCreateRequestDTO, TResponseDTO> service)
    {
        _logger = logger;
        _service = service;
    }
    
    [HttpGet(nameof(GetByIdAsync))]
    public virtual async Task<TResponseDTO> GetByIdAsync(Guid id)
    {
        var result = await _service.GetByIdAsync(id);
        return result;
    }
    
    [HttpGet(nameof(GetAllAsync))]
    public virtual async Task<IEnumerable<TResponseDTO>> GetAllAsync()
    {
        var result = await _service.GetAllAsync();
        return result;
    }
    
    [HttpPost(nameof(CreateAsync))]
    public virtual async Task<TResponseDTO> CreateAsync(TCreateRequestDTO entity)
    {
        var result = await _service.CreateAsync(entity);
        return result;
    }
    
    [HttpPut(nameof(UpdateAsync))]
    public virtual async Task<IActionResult> UpdateAsync(TCreateRequestDTO entity)
    {
        var result = await _service.UpdateAsync(entity);
        return result ? Ok($"Updated Successfully") : BadRequest($"Failed Updating");
    }

    [HttpDelete(nameof(DeleteByIdAsync))]
    public virtual async Task<IActionResult> DeleteByIdAsync(Guid id)
    {
        var result = await _service.DeleteByIdAsync(id);
        return result ? Ok($"{id} Deleted Successfully") : BadRequest($"Failed Deleting {id}");
    }
}
