using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace UseCases.Users;

/// <summary>
/// Handler for <inheritdoc cref="SignOutQuery"/>.
/// </summary>
internal class SignOutQueryHandler : AsyncRequestHandler<SignOutQuery>
{
    private readonly SignInManager<User> signInManager;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="signInManager">Sign in manager.</param>
    public SignOutQueryHandler(SignInManager<User> signInManager)
    {
        this.signInManager = signInManager;
    }

    /// <inheritdoc/>
    protected override async Task Handle(SignOutQuery request, CancellationToken cancellationToken)
    {
        await signInManager.SignOutAsync();
    }
}
