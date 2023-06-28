using Microsoft.AspNetCore.Mvc;
using UserManagement.Services.Interfaces;

namespace UserManagement.Controllers;

[ApiController]
[Route("[controller]")]
public class EntityBaseController<TCreateRequestDTO, TResponseDTO> : ControllerBase
{
    protected readonly IEntityService<TCreateRequestDTO, TResponseDTO> _service;

    public EntityBaseController(ILogger<EntityBaseController<TCreateRequestDTO, TResponseDTO>> logger, IEntityService<TCreateRequestDTO, TResponseDTO> service)
    {
        _service = service;
    }
    
    [HttpGet]
    public virtual async Task<TResponseDTO> GetByIdAsync([FromQuery] Guid id)
    {
        var result = await _service.GetByIdAsync(id);
        return result;
    }
    
    [HttpGet]
    public virtual async Task<IEnumerable<TResponseDTO>> GetAllAsync()
    {
        var result = await _service.GetAllAsync();
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
        return result ? Ok("Updated Successfully") : BadRequest("Failed Updating");
    }

    [HttpDelete]
    public virtual async Task<IActionResult> DeleteByIdAsync([FromQuery] Guid id)
    {
        var result = await _service.DeleteByIdAsync(id);
        return result ? Ok($"{id} Deleted Successfully") : BadRequest($"Failed Deleting {id}");
    }
}
