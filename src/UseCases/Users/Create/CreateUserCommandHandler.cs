using Domain;
using Infrastructure.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Saritasa.Tools.Common.Utils;
using Saritasa.Tools.Domain.Exceptions;

namespace UseCases.Users;

/// <summary>
/// Handler for <inheritdoc cref="CreateUserCommand"/>.
/// </summary>
internal class CreateUserCommandHandler : AsyncRequestHandler<CreateUserCommand>
{
    private readonly UserManager<User> userManager;
    private readonly IUserAccessor userAccessor;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="userManager">User manager.</param>
    public CreateUserCommandHandler(UserManager<User> userManager, IUserAccessor userAccessor)
    {
        this.userManager = userManager;
        this.userAccessor = userAccessor;
    }

    /// <inheritdoc/>
    protected override async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (request.RegistrationModel.BirthDay?.ToUniversalTime() > DateTime.UtcNow)
        {
            throw new ValidationException("Invalid date.");
        }

        var creator = await userAccessor.GetMeAsync(cancellationToken);

        var user = new User
        {
            FirstName = request.RegistrationModel.FirstName,
            LastName = request.RegistrationModel.LastName,
            BirthDay = request.RegistrationModel.BirthDay?.ToUniversalTime(),
            UserName = request.RegistrationModel.Login,
            CreatorId = creator.Id,
            CreatedOn = DateTime.UtcNow,
            Gender = request.RegistrationModel.Gender,
        };
        var response = await userManager.CreateAsync(user, request.RegistrationModel.Password);
        if (!response.Succeeded)
        {
            throw new DomainException(StringUtils.JoinIgnoreEmpty(", ", response.Errors.Select(err=>err.Description)));
        }
    }
}
