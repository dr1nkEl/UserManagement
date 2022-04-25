using AutoMapper;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Saritasa.Tools.EFCore;
using UseCases.Common.User;

namespace UseCases.Users;

/// <summary>
/// Handler for <inheritdoc cref="GetUserQuery"/>.
/// </summary>
internal class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto>
{
    private readonly IMapper mapper;
    private readonly UserManager<User> userManager;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="mapper">Mapper.</param>
    /// <param name="userManager">User manager.</param>
    public GetUserQueryHandler(IMapper mapper, UserManager<User> userManager)
    {
        this.mapper = mapper;
        this.userManager = userManager;
    }

    /// <inheritdoc/>
    public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        return await mapper
            .ProjectTo<UserDto>(userManager.Users)
            .GetAsync(user => user.UserName == request.Login, cancellationToken);
    }
}
