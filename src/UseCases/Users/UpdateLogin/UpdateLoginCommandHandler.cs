using Domain;
using Infrastructure.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Saritasa.Tools.Common.Utils;
using Saritasa.Tools.Domain.Exceptions;
using Saritasa.Tools.EFCore;

namespace UseCases.Users.UpdateLogin;

/// <summary>
/// Handler for <inheritdoc cref="UpdateLoginCommand"/>.
/// </summary>
internal class UpdateLoginCommandHandler : AsyncRequestHandler<UpdateLoginCommand>
{
    private readonly UserManager<User> userManager;
    private readonly IUserAccessor userAccessor;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="userManager">User manager.</param>
    /// <param name="userAccessor">User accessor.</param>
    public UpdateLoginCommandHandler(UserManager<User> userManager, IUserAccessor userAccessor)
    {
        this.userManager = userManager;
        this.userAccessor = userAccessor;
    }

    /// <inheritdoc/>
    protected override async Task Handle(UpdateLoginCommand request, CancellationToken cancellationToken)
    {
        if (!await userAccessor.CanEditAsync(request.Dto.Id, cancellationToken))
        {
            throw new ValidationException("Cant edit given user.");
        }
        var user = await userManager.Users.GetAsync(user => user.Id == request.Dto.Id, cancellationToken);

        var res = await userManager.SetUserNameAsync(user, request.Dto.Login);

        if (!res.Succeeded)
        {
            throw new DomainException(StringUtils.JoinIgnoreEmpty(", ", res.Errors.Select(err => err.Description)));
        }

        await userAccessor.SetLastModifiedAsync(request.Dto.Id, cancellationToken);
    }
}
