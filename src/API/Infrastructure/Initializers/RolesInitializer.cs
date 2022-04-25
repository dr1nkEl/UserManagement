using Domain;
using Extensions.Hosting.AsyncInitialization;
using Microsoft.AspNetCore.Identity;

namespace API.Infrastructure.Initializers;

/// <summary>
/// Roles initalizer.
/// </summary>
public class RolesInitializer : IAsyncInitializer
{
    private readonly RoleManager<IdentityRole<int>> roleManager;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="roleManager">Role manager.</param>
    public RolesInitializer(RoleManager<IdentityRole<int>> roleManager)
    {
        this.roleManager = roleManager;
    }

    /// <inheritdoc/>
    public async Task InitializeAsync()
    {
        if (!await roleManager.RoleExistsAsync(ApplicationRoles.Admin))
        {
            await roleManager.CreateAsync(new IdentityRole<int>
            {
                Name = ApplicationRoles.Admin,
            });
        }
    }
}
