using Domain;
using Infrastructure.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Saritasa.Tools.Common.Utils;
using Saritasa.Tools.Domain.Exceptions;
using Saritasa.Tools.EFCore;

namespace UseCases.Users.Delete;

/// <summary>
/// Handler for <inheritdoc cref="DeleteUserCommand"/>.
/// </summary>
internal class DeleteUserCommandHandler : AsyncRequestHandler<DeleteUserCommand>
{
    private readonly UserManager<User> userManager;
    private readonly IUserAccessor userAccessor;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="userManager">User manager.</param>
    /// <param name="userAccessor">User accessor.</param>
    public DeleteUserCommandHandler(UserManager<User> userManager, IUserAccessor userAccessor)
    {
        this.userManager = userManager;
        this.userAccessor = userAccessor;
    }

    /// <inheritdoc/>
    protected override async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.Users.GetAsync(user => user.UserName == request.Login, cancellationToken);
        var curUser = await userAccessor.GetMeAsync(cancellationToken);
        user.DeletedAt = DateTime.UtcNow;
        user.RevokedOn = DateTime.UtcNow;
        user.RevokedById = curUser.Id;
        user.ModifiedById = curUser.Id;
        user.ModifiedOn = DateTime.UtcNow;
        var res = await userManager.UpdateAsync(user);
        if (!res.Succeeded)
        {
            throw new DomainException(StringUtils.JoinIgnoreEmpty(", ", res.Errors.Select(err => err.Description)));
        }
    }
}
