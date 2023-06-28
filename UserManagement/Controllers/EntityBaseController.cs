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
    
    [HttpGet]
    public virtual async Task<TResponseDTO> GetByIdAsync([FromQuery] Guid id)
    {
        var result = await _service.GetByIdAsync(id);
        return result;
    }
    
    [HttpPost]
    public virtual async Task<TResponseDTO> CreateAsync([FromBody] TCreateRequestDTO entity)
    {
        var result = await _service.CreateAsync(entity);
        return result;
    }
    
    [HttpPut]
    public virtual async Task<IActionResult> UpdateAsync([FromQuery] Guid id, [FromBody] TCreateRequestDTO entity)
    {
        var result = await _service.UpdateAsync(id, entity);
        return result ? Ok($"Updated Successfully") : BadRequest($"Failed Updating");
    }

    [HttpDelete]
    public virtual async Task<IActionResult> DeleteByIdAsync([FromQuery] Guid id)
    {
        var result = await _service.DeleteByIdAsync(id);
        return result ? Ok($"{id} Deleted Successfully") : BadRequest($"Failed Deleting {id}");
    }
}
