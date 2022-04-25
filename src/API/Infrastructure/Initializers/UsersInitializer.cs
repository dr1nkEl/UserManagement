using API.Infrastructure.Models;
using Domain;
using Extensions.Hosting.AsyncInitialization;
using Infrastructure.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Saritasa.Tools.Common.Utils;
using Saritasa.Tools.Domain.Exceptions;

namespace API.Infrastructure.Initializers;

/// <summary>
/// Users initializer.
/// </summary>
public class UsersInitializer : IAsyncInitializer
{
    private const string AdminLogin = "admin";
    private const string AdminPassword = "admin";
    private readonly UserManager<User> userManager;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="userManager">User manager.</param>
    public UsersInitializer(UserManager<User> userManager)
    {
        this.userManager = userManager;
    }

    /// <inheritdoc/>
    public async Task InitializeAsync()
    {
        if (!await userManager.Users.AnyAsync(user=>user.UserName == AdminLogin, CancellationToken.None))
        {
            var user = new User
            {
                FirstName = "Admin",
                LastName = "Admin",
                UserName = AdminLogin,
            };

            var res= await userManager.CreateAsync(user, AdminPassword);
            await userManager.AddToRoleAsync(user, ApplicationRoles.Admin);
        }
    }
}
