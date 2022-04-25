using AutoMapper;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UseCases.Common.User;

namespace UseCases.Users;

/// <summary>
/// Handler for <inheritdoc cref="GetUsersQuery"/>.
/// </summary>
internal class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<UserDto>>
{
    private readonly UserManager<User> userManager;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="userManager">User manager.</param>
    /// <param name="mapper">Mapper.</param>
    public GetUsersQueryHandler(UserManager<User> userManager, IMapper mapper)
    {
        this.userManager = userManager;
        this.mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        return await mapper.ProjectTo<UserDto>(userManager.Users)
            .Where(user => user.RevokedOn == null)
            .OrderBy(user => user.CreatedOn)
            .ToListAsync(cancellationToken);
    }
}
