using Domain;
using Infrastructure.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Saritasa.Tools.Common.Utils;
using Saritasa.Tools.Domain.Exceptions;
using Saritasa.Tools.EFCore;

namespace UseCases.Users.Recover;

/// <summary>
/// Handler for <inheritdoc cref="RecoverUserCommand"/>.
/// </summary>
internal class RecoverUserCommandHandler : AsyncRequestHandler<RecoverUserCommand>
{
    private readonly UserManager<User> userManager;
    private readonly IUserAccessor userAccessor;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="userManager">User manager.</param>
    /// <param name="userAccessor">User accessor.</param>
    public RecoverUserCommandHandler(UserManager<User> userManager, IUserAccessor userAccessor)
    {
        this.userManager = userManager;
        this.userAccessor = userAccessor;
    }

    /// <inheritdoc/>
    protected override async Task Handle(RecoverUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.Users.GetAsync(user => user.Id == request.Id, cancellationToken);
        user.RevokedById = null;
        user.RevokedOn = null;
        var res = await userManager.UpdateAsync(user);
        if (!res.Succeeded)
        {
            throw new DomainException(StringUtils.JoinIgnoreEmpty(", ", res.Errors.Select(err => err.Description)));
        }
        await userAccessor.SetLastModifiedAsync(user.Id, cancellationToken);
    }
}
