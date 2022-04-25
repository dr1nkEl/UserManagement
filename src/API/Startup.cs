using Microsoft.AspNetCore.Identity;
using API.Infrastructure.Initializers;
using Infrastructure.DataAccess;
using API.Infrastructure.Startup;
using Domain;
using Infrastructure.Abstractions;
using Infrastructure;
using MediatR;
using API.Infrastructure.Middleware;
using API.Infrastructure.MappingProfiles;
using UseCases.Users;

namespace API;

/// <summary>
/// Entry point for ASP.NET Core app.
/// </summary>
public class Startup
{
    private readonly IConfiguration configuration;

    /// <summary>
    /// Entry point for web application.
    /// </summary>
    /// <param name="configuration">Global configuration.</param>
    public Startup(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    /// <summary>
    /// Configure application services on startup.
    /// </summary>
    /// <param name="services">Services to configure.</param>
    /// <param name="environment">Application environment.</param>
    public void ConfigureServices(IServiceCollection services)
    {
        // Add controllers.
        services.AddControllers();

        // Database.
        services.AddDbContext<AppDbContext>(
            new DbContextOptionsSetup(configuration.GetConnectionString("AppDatabase")).Setup);
        services.AddAsyncInitializer<DatabaseInitializer>();

        // Swagger.
        services.AddSwaggerGen();

        // Identity.
        services.AddIdentity<User, IdentityRole<int>>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
        services.Configure<IdentityOptions>(new IdentityOptionsSetup().Setup);
        services.AddAsyncInitializer<RolesInitializer>();
        services.AddAsyncInitializer<UsersInitializer>();

        // Add authentication.
        services.AddAuthentication();

        // Add authorization
        services.AddAuthorization();

        // Automapper.
        services.AddAutoMapper(typeof(UserMappingProfile).Assembly);

        // Other dependencies.
        services.AddMediatR(typeof(CreateUserCommand).Assembly);
        services.AddTransient<IUserAccessor, UserAccessor>();
    }

    /// <summary>
    /// Configure web application.
    /// </summary>
    /// <param name="app">Application builder.</param>
    /// <param name="environment">Application environment.</param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment environment)
    {
        if (!environment.IsDevelopment())
        {
            app.UseHsts();
        }
        app.UseMiddleware<ApiExceptionMiddleware>();
        app.UseStaticFiles();
        app.UseHttpsRedirection();
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapSwagger();
            endpoints.MapControllers();
        });
    }
}
