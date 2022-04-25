using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess;

/// <summary>
/// Application database context.
/// </summary>
// I did not make abstraction for context
// because it is never used so it has no purpose to exist(at least for now).
public class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="options">Options.</param>
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        RestrictCascadeDelete(builder);

        builder.Entity<User>()
            .HasQueryFilter(user => user.DeletedAt == null);
    }

    private static void RestrictCascadeDelete(ModelBuilder modelBuilder)
    {
        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
}
