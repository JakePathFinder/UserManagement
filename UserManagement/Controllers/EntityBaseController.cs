using Microsoft.AspNetCore.Mvc;
using UserManagement.Services.Interfaces;

namespace UserManagement.Controllers;

[ApiController]
[Route("[controller]")]
public class EntityBaseController<TCreateRequestDTO, TResponseDTO> : ControllerBase
{
    protected readonly ILogger<EntityBaseController<TCreateRequestDTO, TResponseDTO>> Logger;
    protected readonly IEntityService<TCreateRequestDTO, TResponseDTO> Service;

    public EntityBaseController(ILogger<EntityBaseController<TCreateRequestDTO, TResponseDTO>> logger, IEntityService<TCreateRequestDTO, TResponseDTO> service)
    {
        Logger = logger;
        Service = service;
    }
    
    [HttpGet($"{{{nameof(id)}}}")]
    public virtual async Task<TResponseDTO> GetByIdAsync([FromRoute] Guid id)
    {
        Logger.LogInformation("{methodName} invoked with {id}", nameof(GetByIdAsync), id);
        var result = await Service.GetByIdAsync(id);
        return result;
    }
    
    [HttpGet()]
    public virtual async Task<IEnumerable<TResponseDTO>> GetAllAsync()
    {
        Logger.LogInformation("{methodName} invoked", nameof(GetByIdAsync));
        var result = await Service.GetAllAsync();
        return result;
    }
	
	[HttpPost]
    public virtual async Task<TResponseDTO> CreateAsync([FromBody] TCreateRequestDTO entity)
    {
        Logger.LogInformation("{methodName} invoked", nameof(GetByIdAsync));
        var result = await Service.CreateAsync(entity);
        return result;
    }
    
    [HttpPut($"{{{nameof(id)}}}")]
    public virtual async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] TCreateRequestDTO entity)
    {
        Logger.LogInformation("{methodName} invoked with {id}", nameof(GetByIdAsync), id);
        var result = await Service.UpdateAsync(id, entity);
        return result ? Ok("Updated Successfully") : BadRequest("Failed Updating");
    }

    [HttpDelete($"{{{nameof(id)}}}")]
    public virtual async Task<IActionResult> DeleteByIdAsync([FromRoute] Guid id)
    {
        Logger.LogInformation("{methodName} invoked with {id}", nameof(GetByIdAsync), id);
        var result = await Service.DeleteByIdAsync(id);
        return result ? Ok($"{id} Deleted Successfully") : BadRequest($"Failed Deleting {id}");
    }
}
