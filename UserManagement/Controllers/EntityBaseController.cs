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
    public virtual async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
    {
        Logger.LogInformation("{methodName} invoked with {id}", nameof(GetByIdAsync), id);
        var response = await Service.GetByIdAsync(id);
        return ToIActionResult(response);
    }
    
    [HttpGet]
    [SwaggerOperation(Summary = "Retrieves all items")]
    public virtual async Task<IActionResult> GetAllAsync()
    {
        Logger.LogInformation("{methodName} invoked", nameof(GetByIdAsync));
        var responses = await Service.GetAllAsync();
        return ToIActionResult(responses);
    }
	
	[HttpPost]
    [SwaggerOperation(Summary = "Creates a new item")]
    public virtual async Task<IActionResult> CreateAsync([FromBody] TCreateRequestDTO entity)
    {
        Logger.LogInformation("{methodName} invoked", nameof(GetByIdAsync));
        var response = await Service.CreateAsync(entity);
        return ToIActionResult(response);
    }
    
    [HttpPut($"{{{nameof(id)}}}")]
    [SwaggerOperation(Summary = "Updates an existing item")]
    public virtual async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] TCreateRequestDTO entity)
    {
        Logger.LogInformation("{methodName} invoked with {id}", nameof(GetByIdAsync), id);
        var response = await Service.UpdateAsync(id, entity);
        return ToIActionResult(response);
    }

    [HttpDelete($"{{{nameof(id)}}}")]
    [SwaggerOperation(Summary = "Deletes an item")]
    public virtual async Task<IActionResult> DeleteByIdAsync([FromRoute] Guid id)
    {
        Logger.LogInformation("{methodName} invoked with {id}", nameof(GetByIdAsync), id);
        var response = await Service.DeleteAsync(id);
        return ToIActionResult(response);
    }

    [HttpPost(nameof(Bulk))]
    [SwaggerOperation(Summary = "Bulk Operation on Items")]
    public virtual async Task<IActionResult> Bulk([FromBody] BulkOperationRequest bulkOperationRequest)
    {
        Logger.LogInformation("{methodName} {operationType} invoked", nameof(Bulk), bulkOperationRequest.OperationType);
        var bulkOperationResponse = await Service.Bulk(bulkOperationRequest);
        return ToIActionResult(bulkOperationResponse);
    }

    private IActionResult ToIActionResult(Response<TResponseDTO> response)
    {
        return response.Success ? Ok(response) : BadRequest(response);
    }

    private IActionResult ToIActionResult(BulkOperationResponse<TResponseDTO> bulkOperationResponse)
    {
        return bulkOperationResponse.Success() ? Ok(bulkOperationResponse) : BadRequest(bulkOperationResponse);
    }
}
