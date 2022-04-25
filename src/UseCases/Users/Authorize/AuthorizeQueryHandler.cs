using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Saritasa.Tools.Domain.Exceptions;
using Saritasa.Tools.EFCore;

namespace UseCases.Users.Authorize;

/// <summary>
/// Handler for <inheritdoc cref="AuthorizeQuery"/>.
/// </summary>
internal class AuthorizeQueryHandler : AsyncRequestHandler<AuthorizeQuery>
{
    private readonly SignInManager<User> signInManager;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="signInManager">Sign in manager.</param>
    public AuthorizeQueryHandler(SignInManager<User> signInManager)
    {
        this.signInManager = signInManager;
    }

    /// <inheritdoc/>
    protected override async Task Handle(AuthorizeQuery request, CancellationToken cancellationToken)
    {
        var user = await signInManager.UserManager.Users.GetAsync(user => user.UserName == request.UserCredentials.Login, cancellationToken);
        if (user.RevokedOn != null)
        {
            throw new ValidationException("Revoked users cant authorize!");
        }
        var response = await signInManager.PasswordSignInAsync(user, request.UserCredentials.Password, false, false);
        if (!response.Succeeded)
        {
            throw new DomainException("Failed to authorize. Check your credentials!");
        }
    }
}
