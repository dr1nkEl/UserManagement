using AutoMapper;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UseCases.Common.User;

namespace UseCases.Users.GetOlderThan;

/// <summary>
/// Handler for <inheritdoc cref="GetUsersOlderThanQuery"/>.
/// </summary>
internal class GetUsersOlderThanQueryHandler : IRequestHandler<GetUsersOlderThanQuery, IEnumerable<UserDto>>
{
    private readonly UserManager<User> userManager;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="userManager">User manager.</param>
    /// <param name="mapper">Mapper.</param>
    public GetUsersOlderThanQueryHandler(UserManager<User> userManager, IMapper mapper)
    {
        this.userManager = userManager;
        this.mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<UserDto>> Handle(GetUsersOlderThanQuery request, CancellationToken cancellationToken)
    {
        return await mapper
            .ProjectTo<UserDto>(userManager.Users)
            .Where(user => user.BirthDay.HasValue && user.RevokedOn == null && user.BirthDay.Value < request.Date.ToUniversalTime())
            .ToListAsync(cancellationToken);
    }
}
