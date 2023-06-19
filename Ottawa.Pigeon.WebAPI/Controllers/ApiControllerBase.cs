using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Ottawa.Pigeon.Controllers;

/// <summary>
/// Base API Controller
/// </summary>
[ApiController]
[Route("api/v{version:apiVersion}/[controller]s")]
public abstract class ApiControllerBase : ControllerBase
{
    private ISender _mediator = null!;

    /// <summary>
    /// return Mediator Services
    /// </summary>
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}
