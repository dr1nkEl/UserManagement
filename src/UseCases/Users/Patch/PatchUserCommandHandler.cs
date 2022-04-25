using Domain;
using Infrastructure.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Saritasa.Tools.Common.Utils;
using Saritasa.Tools.Domain.Exceptions;
using Saritasa.Tools.EFCore;

namespace UseCases.Users;

/// <summary>
/// Handler for <inheritdoc cref="PatchUserCommand"/>.
/// </summary>
internal class PatchUserCommandHandler : AsyncRequestHandler<PatchUserCommand>
{
    private readonly UserManager<User> userManager;
    private readonly IUserAccessor userAccessor;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="userManager">User manager.</param>
    /// <param name="appDbContext">Application DB context.</param>
    public PatchUserCommandHandler(UserManager<User> userManager, IUserAccessor userAccessor)
    {
        this.userManager = userManager;
        this.userAccessor = userAccessor;
    }

    /// <inheritdoc/>
    protected override async Task Handle(PatchUserCommand request, CancellationToken cancellationToken)
    {
        if (!await userAccessor.CanEditAsync(request.Dto.Id, cancellationToken))
        {
            throw new ValidationException("Cant edit given user.");
        }

        if (request.Dto.BirthDay?.ToUniversalTime() > DateTime.UtcNow)
        {
            throw new ValidationException("Invalid date.");
        }

        var user = await userManager.Users.GetAsync(user=>user.Id == request.Dto.Id, cancellationToken);
        var curUser = await userAccessor.GetMeAsync(cancellationToken);
        user.BirthDay = request.Dto.BirthDay?.ToUniversalTime();
        user.FirstName = request.Dto.FirstName;
        user.LastName = request.Dto.LastName;
        user.Gender = request.Dto.Gender;
        var res = await userManager.UpdateAsync(user);
        if (!res.Succeeded)
        {
            throw new DomainException(StringUtils.JoinIgnoreEmpty(", ", res.Errors.Select(err=>err.Description)));
        }

        await userAccessor.SetLastModifiedAsync(request.Dto.Id, cancellationToken);
    }
}
