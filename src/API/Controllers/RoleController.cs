using API.Infrastructure.Models;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UseCases.Common.Role;
using UseCases.Role;

namespace API.Controllers;

/// <summary>
/// Role API controller.
/// </summary>
[Route("api/[controller]/[action]")]
[ApiController]
[Authorize(Roles = ApplicationRoles.Admin)]
public class RoleController : ControllerBase
{
    private readonly IMediator mediator;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="mediator">Mediator.</param>
    /// <param name="mapper">Mapper.</param>
    public RoleController(IMediator mediator, IMapper mapper)
    {
        this.mediator = mediator;
        this.mapper = mapper;
    }

    /// <summary>
    /// GET application roles.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Status code and execution result.</returns>
    [HttpGet]
    [ProducesResponseType(200)]
    [Produces("application/json")]
    public async Task<ActionResult<IEnumerable<IdentityRole<int>>>> All(CancellationToken cancellationToken)
    {
        return Ok(await mediator.Send(new GetRolesQuery(), cancellationToken));
    }

    /// <summary>
    /// POST add user to role.
    /// </summary>
    /// <param name="model">Model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Status code.</returns>
    [HttpPost]
    [ProducesResponseType(200)]
    [Produces("application/json")]
    public async Task<ActionResult> AddUserToRole([FromQuery]AddUserToRole model, CancellationToken cancellationToken)
    {
        var dto = mapper.Map<AddUserToRoleDto>(model);
        await mediator.Send(new AddUserToRoleCommand(dto), cancellationToken);
        return Ok("Added user to role.");
    }
}
