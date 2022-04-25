using Domain;
using Infrastructure.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Saritasa.Tools.Common.Utils;
using Saritasa.Tools.Domain.Exceptions;
using Saritasa.Tools.EFCore;

namespace UseCases.Users;

/// <summary>
/// Handler for <inheritdoc cref="RevokeUserCommand"/>.
/// </summary>
internal class RevokeUserCommandHandler : AsyncRequestHandler<RevokeUserCommand>
{
    private readonly UserManager<User> userManager;
    private readonly IUserAccessor userAccessor;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="userManager">User manager.</param>
    /// <param name="userAccessor">User accessor.</param>
    public RevokeUserCommandHandler(UserManager<User> userManager, IUserAccessor userAccessor)
    {
        this.userManager = userManager;
        this.userAccessor = userAccessor;
    }

    /// <inheritdoc/>
    protected override async Task Handle(RevokeUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.Users.GetAsync(user => user.Id == request.Id, cancellationToken);
        var curUser = await userAccessor.GetMeAsync(cancellationToken);
        user.RevokedById = curUser.Id;
        user.RevokedOn = DateTime.UtcNow;
        var res = await userManager.UpdateAsync(user);
        if (!res.Succeeded)
        {
            throw new DomainException(StringUtils.JoinIgnoreEmpty(", ", res.Errors.Select(err => err.Description)));
        }
    }
}
