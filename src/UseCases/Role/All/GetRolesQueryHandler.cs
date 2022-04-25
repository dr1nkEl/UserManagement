using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UseCases.Common.Role;

namespace UseCases.Role;

/// <summary>
/// Handler for <inheritdoc cref="GetRolesQuery"/>.
/// </summary>
internal class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, IEnumerable<RoleDto>>
{
    private readonly RoleManager<IdentityRole<int>> roleManager;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="roleManager">Role manager.</param>
    /// <param name="mapper">Mapper.</param>
    public GetRolesQueryHandler(RoleManager<IdentityRole<int>> roleManager, IMapper mapper)
    {
        this.roleManager = roleManager;
        this.mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<RoleDto>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        return await mapper.ProjectTo<RoleDto>(roleManager.Roles).ToListAsync(cancellationToken);
    }
}
