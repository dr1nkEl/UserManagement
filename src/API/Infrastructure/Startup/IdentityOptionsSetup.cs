using Microsoft.AspNetCore.Identity;

namespace API.Infrastructure.Startup;

/// <summary>
/// Identity options setup.
/// </summary>
public class IdentityOptionsSetup
{
    /// <summary>
    /// Setup identity.
    /// </summary>
    /// <param name="options">The options.</param>
    public void Setup(IdentityOptions options)
    {
        // Those settings are for easier debug and testing purposes in this test task.
        // Surely, its not the settings for prod/etc.
        options.User.RequireUniqueEmail = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireDigit = false;
        options.Password.RequireUppercase = false;
        options.Password.RequiredLength = 1;
    }
}
