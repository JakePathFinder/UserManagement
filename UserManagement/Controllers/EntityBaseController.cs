using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UserManagement.DTO;
using UserManagement.Services.Interfaces;

namespace UserManagement.Controllers;

[ApiController]
[Route("[controller]")]
public class EntityBaseController<TCreateRequestDTO, TResponseDTO> : ControllerBase where TResponseDTO : class
{
    protected readonly ILogger<EntityBaseController<TCreateRequestDTO, TResponseDTO>> Logger;
    protected readonly IEntityService<TCreateRequestDTO, TResponseDTO> Service;

    public EntityBaseController(ILogger<EntityBaseController<TCreateRequestDTO, TResponseDTO>> logger, IEntityService<TCreateRequestDTO, TResponseDTO> service)
    {
        Logger = logger;
        Service = service;
    }
    
    [HttpGet($"{{{nameof(id)}}}")]
    [SwaggerOperation(Summary = "Retrieves an item by id")]
    public virtual async Task<Response<TResponseDTO>> GetByIdAsync([FromRoute] Guid id)
    {
        Logger.LogInformation("{methodName} invoked with {id}", nameof(GetByIdAsync), id);
        var result = await Service.GetByIdAsync(id);
        return result;
    }
    
    [HttpGet]
    [SwaggerOperation(Summary = "Retrieves all items")]
    public virtual async Task<IEnumerable<TResponseDTO>> GetAllAsync()
    {
        Logger.LogInformation("{methodName} invoked", nameof(GetByIdAsync));
        var result = await Service.GetAllAsync();
        return result;
    }
	
	[HttpPost]
    [SwaggerOperation(Summary = "Creates a new item")]
    public virtual async Task<Response<TResponseDTO>> CreateAsync([FromBody] TCreateRequestDTO entity)
    {
        Logger.LogInformation("{methodName} invoked", nameof(GetByIdAsync));
        var result = await Service.CreateAsync(entity);
        return result;
    }
    
    [HttpPut($"{{{nameof(id)}}}")]
    [SwaggerOperation(Summary = "Updates an existing item")]
    public virtual async Task<Response<TResponseDTO>> UpdateAsync([FromRoute] Guid id, [FromBody] TCreateRequestDTO entity)
    {
        Logger.LogInformation("{methodName} invoked with {id}", nameof(GetByIdAsync), id);
        var result = await Service.UpdateAsync(id, entity);
        return result;
    }

    [HttpDelete($"{{{nameof(id)}}}")]
    [SwaggerOperation(Summary = "Deletes an item")]
    public virtual async Task<IActionResult> DeleteByIdAsync([FromRoute] Guid id)
    {
        Logger.LogInformation("{methodName} invoked with {id}", nameof(GetByIdAsync), id);
        var result = await Service.DeleteAsync(id);
        return result.Success ? Ok($"{id} Deleted Successfully") : BadRequest($"Failed Deleting {id}");
    }

    /*[HttpPost(nameof(Bulk))]
    [SwaggerOperation(Summary = "Bulk Operation on Items")]
    public virtual async Task<IActionResult> Bulk([FromBody] BulkOperationRequest bulkOperationRequest)
    {
        Logger.LogInformation("{methodName} {operationType} invoked", nameof(Bulk), bulkOperationRequest.OperationType);
        var result = await Service.DeleteByIdAsync(id);
        return result ? Ok($"{id} Deleted Successfully") : BadRequest($"Failed Deleting {id}");
    }*/
}
