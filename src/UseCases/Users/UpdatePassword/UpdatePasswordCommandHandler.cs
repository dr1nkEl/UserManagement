using Domain;
using Infrastructure.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Saritasa.Tools.Common.Utils;
using Saritasa.Tools.Domain.Exceptions;
using Saritasa.Tools.EFCore;

namespace UseCases.Users.UpdatePassword;

/// <summary>
/// Handler for <inheritdoc cref="UpdatePasswordCommand"/>.
/// </summary>
internal class UpdatePasswordCommandHandler : AsyncRequestHandler<UpdatePasswordCommand>
{
    private readonly UserManager<User> userManager;
    private readonly IUserAccessor userAccessor;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="userManager">User manager.</param>
    /// <param name="userAccessor">User accessor.</param>
    public UpdatePasswordCommandHandler(UserManager<User> userManager, IUserAccessor userAccessor)
    {
        this.userManager = userManager;
        this.userAccessor = userAccessor;
    }

    /// <inheritdoc/>
    protected override async Task Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
    {
        if (!await userAccessor.CanEditAsync(request.Dto.Id, cancellationToken))
        {
            throw new ValidationException("Cant edit given user.");
        }
        var user = await userManager.Users.GetAsync(user => user.Id == request.Dto.Id, cancellationToken);
        var curUser = await userAccessor.GetMeAsync(cancellationToken);
        var removeResponse = await userManager.RemovePasswordAsync(user);
        if (!removeResponse.Succeeded)
        {
            throw new DomainException(StringUtils.JoinIgnoreEmpty(", ", removeResponse.Errors.Select(err => err.Description)));
        }
        var setResponse = await userManager.AddPasswordAsync(user, request.Dto.Password);
        if (!setResponse.Succeeded)
        {
            throw new DomainException(StringUtils.JoinIgnoreEmpty(", ", setResponse.Errors.Select(err => err.Description)));
        }

        await userAccessor.SetLastModifiedAsync(request.Dto.Id, cancellationToken);
    }
}
