using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Saritasa.Tools.Common.Utils;
using Saritasa.Tools.Domain.Exceptions;
using Saritasa.Tools.EFCore;

namespace UseCases.Role;

/// <summary>
/// Handler for <inheritdoc cref="AddUserToRoleCommand"/>.
/// </summary>
internal class AddUserToRoleCommandHandler : AsyncRequestHandler<AddUserToRoleCommand>
{
    private readonly UserManager<User> userManager;
    private readonly RoleManager<IdentityRole<int>> roleManager;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="userManager">User manager.</param>
    /// <param name="roleManager">Role manager.</param>
    public AddUserToRoleCommandHandler(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
    {
        this.userManager = userManager;
        this.roleManager = roleManager;
    }

    /// <inheritdoc/>
    protected override async Task Handle(AddUserToRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await roleManager.Roles.GetAsync(role => role.Id == request.Dto.RoleId, cancellationToken);
        var user = await userManager.Users.GetAsync(user=> user.Id == request.Dto.UserId, cancellationToken);
        var res = await userManager.AddToRoleAsync(user, role.Name);
        if (!res.Succeeded)
        {
            throw new DomainException(StringUtils.JoinIgnoreEmpty(", ", res.Errors.Select(err => err.Description)));
        }
    }
}
