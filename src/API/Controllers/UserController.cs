using API.Infrastructure.Models;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using UseCases.Common.User;
using UseCases.Users;

namespace API.Controllers;

/// <summary>
/// User API controller.
/// </summary>
[ApiController]
[Route("api/[controller]/[action]")]
public class UserController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly IMediator mediator;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="mapper">Mapper.</param>
    /// <param name="mediator">Mediator.</param>
    public UserController(IMapper mapper, IMediator mediator)
    {
        this.mapper = mapper;
        this.mediator = mediator;
    }

    /// <summary>
    /// POST register action.
    /// </summary>
    /// <param name="credentials">Credentials.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Status code.</returns>
    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Produces("application/json")]
    public async Task<ActionResult> Register([FromQuery] UserRegister registerModel, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var dto = mapper.Map<UserRegisterDto>(registerModel);
        await mediator.Send(new CreateUserCommand(dto), cancellationToken);
        return Ok("Registered.");
    }

    /// <summary>
    /// POST authorize action.
    /// </summary>
    /// <param name="credentials">Credentials.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Status code.</returns>
    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Produces("application/json")]
    public async Task<ActionResult> Authorize([FromQuery]UserCredentials credentials, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var dto = mapper.Map<UserCredentialsDto>(credentials);
        await mediator.Send(new AuthorizeQuery(dto), cancellationToken);
        return Ok("Authorized.");
    }

    /// <summary>
    /// POST logout action.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Status code.</returns>
    [Authorize]
    [HttpPost]
    [ProducesResponseType(200)]
    [Produces("application/json")]
    public async Task<ActionResult> Logout(CancellationToken cancellationToken)
    {
        await mediator.Send(new SignOutQuery(), cancellationToken);
        return Ok("Logged out.");
    }

    /// <summary>
    /// PATCH user.
    /// </summary>
    /// <param name="model">Model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Status code.</returns>
    [Authorize]
    [HttpPatch]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Produces("application/json")]
    public async Task<ActionResult> UpdateInfo([FromQuery]PatchUser model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var dto = mapper.Map<PatchUserDto>(model);
        await mediator.Send(new PatchUserCommand(dto), cancellationToken);
        return Ok("Patched.");
    }

    /// <summary>
    /// GET users.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Status code and execution result.</returns>
    [HttpGet]
    [Authorize(Roles = ApplicationRoles.Admin)]
    [ProducesResponseType(200)]
    [Produces("application/json")]
    public async Task<ActionResult<IEnumerable<Infrastructure.Models.UserDto>>> All(CancellationToken cancellationToken)
    {
        var dtos = await mediator.Send(new GetUsersQuery(), cancellationToken);
        var res = mapper.Map<IEnumerable<Infrastructure.Models.UserDto>>(dtos);
        return Ok(res);
    }

    /// <summary>
    /// POST update login.
    /// </summary>
    /// <param name="model">Model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Status code.</returns>
    [HttpPost]
    [Authorize]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Produces("application/json")]
    public async Task<ActionResult> UpdateLogin([FromQuery]UpdateLogin model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var dto = mapper.Map<UpdateLoginDto>(model);
        await mediator.Send(new UpdateLoginCommand(dto), cancellationToken);
        return Ok("Updated login.");
    }

    /// <summary>
    /// POST update password.
    /// </summary>
    /// <param name="model">Model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Status code.</returns>
    [HttpPost]
    [Authorize]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Produces("application/json")]
    public async Task<ActionResult> UpdatePassword([FromQuery]UpdatePassword model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var dto = mapper.Map<UpdatePasswordDto>(model);
        await mediator.Send(new UpdatePasswordCommand(dto), cancellationToken);
        return Ok("Updated password.");
    }

    /// <summary>
    /// GET all older than.
    /// </summary>
    /// <param name="date">Date.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Users.</returns>
    [HttpGet]
    [Authorize(Roles = ApplicationRoles.Admin)]
    [ProducesResponseType(200)]
    [Produces("application/json")]
    public async Task<ActionResult<IEnumerable<Infrastructure.Models.UserDto>>> AllOlderThan([FromQuery][Required] DateTime? date, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var dtos = await mediator.Send(new GetUsersOlderThanQuery(date.Value), cancellationToken);
        return Ok(mapper.Map<IEnumerable<Infrastructure.Models.UserDto>>(dtos));
    }

    /// <summary>
    /// POST delete.
    /// </summary>
    /// <param name="login">Login.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Status code.</returns>
    [HttpPost]
    [Authorize(Roles = ApplicationRoles.Admin)]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Produces("application/json")]
    public async Task<ActionResult> Delete([FromQuery][Required] string login, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        await mediator.Send(new DeleteUserCommand(login), cancellationToken);
        return Ok("Deleted.");
    }

    /// <summary>
    /// POST recover.
    /// </summary>
    /// <param name="id">ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Status code.</returns>
    [HttpPost]
    [Authorize(Roles = ApplicationRoles.Admin)]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Produces("application/json")]
    public async Task<ActionResult> Recover([FromQuery][Required] int? id, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        await mediator.Send(new RecoverUserCommand(id.Value), cancellationToken);
        return Ok("Recovered.");
    }

    /// <summary>
    /// POST revoke user.
    /// </summary>
    /// <param name="id">ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Status code.</returns>
    [HttpPost]
    [Authorize(Roles = ApplicationRoles.Admin)]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Produces("application/json")]
    public async Task<ActionResult> Revoke([FromQuery][Required] int? id, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        await mediator.Send(new RevokeUserCommand(id.Value), cancellationToken);
        return Ok("Revoked user.");
    }

    /// <summary>
    /// GET user.
    /// </summary>
    /// <param name="Login">Login of user.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>User.</returns>
    [HttpGet]
    [Authorize(Roles = ApplicationRoles.Admin)]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Produces("application/json")]
    public async Task<ActionResult<Infrastructure.Models.UserDto>> Get([FromQuery][Required]string Login, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var userDto = await mediator.Send(new GetUserQuery(Login), cancellationToken);
        return Ok(mapper.Map<Infrastructure.Models.UserDto>(userDto));
    }
}