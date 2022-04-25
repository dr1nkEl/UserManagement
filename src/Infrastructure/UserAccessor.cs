using Domain;
using Infrastructure.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Saritasa.Tools.Common.Utils;
using Saritasa.Tools.Domain.Exceptions;
using Saritasa.Tools.EFCore;

namespace Infrastructure;

/// Implementation of <inheritdoc cref="IUserAccessor"/>
public class UserAccessor : IUserAccessor
{
    private readonly UserManager<User> userManager;
    private readonly IHttpContextAccessor contextAccessor;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="userManager">User manager.</param>
    /// <param name="contextAccessor">Http context accessor.</param>
    public UserAccessor(UserManager<User> userManager, IHttpContextAccessor contextAccessor)
    {
        this.userManager = userManager;
        this.contextAccessor = contextAccessor;
    }

    /// <inheritdoc/>
    public async Task<bool> CanEditAsync(int id, CancellationToken cancellationToken = default)
    {
        var user = await userManager.Users.GetAsync(user => user.Id == id, cancellationToken);
        var curUser = await GetMeAsync(cancellationToken);
        if (user.RevokedOn != null)
        {
            throw new ValidationException("Can not edit revoked user!");
        }

        if (user.Id == curUser.Id)
        {
            return true;
        }

        if (await userManager.IsInRoleAsync(curUser, ApplicationRoles.Admin))
        {
            return true;
        }

        return false;
    }

    /// <inheritdoc/>
    public async Task<User> GetMeAsync(CancellationToken cancellationToken)
    {
        var user = contextAccessor.HttpContext.User;
        if (user is null)
        {
            throw new UnauthorizedException(401);
        }
        return await userManager.GetUserAsync(user);
    }

    /// <inheritdoc/>
    public async Task SetLastModifiedAsync(int id, CancellationToken cancellationToken = default)
    {
        var editedUser = await userManager.Users.GetAsync(user=>user.Id == id, cancellationToken);
        var curUser = await GetMeAsync(cancellationToken);
        editedUser.ModifiedById = curUser.Id;
        editedUser.ModifiedOn = DateTime.UtcNow;
        var res = await userManager.UpdateAsync(editedUser);
        if (!res.Succeeded)
        {
            throw new DomainException(StringUtils.JoinIgnoreEmpty(", ", res.Errors.Select(err => err.Description)));
        }
    }
}
