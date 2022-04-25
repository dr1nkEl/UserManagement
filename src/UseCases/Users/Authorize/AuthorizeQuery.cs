using MediatR;
using UseCases.Common.User;

namespace UseCases.Users;

/// <summary>
/// Authorize command.
/// </summary>
/// <param name="UserCredentials">User credentials.</param>
public record AuthorizeQuery(UserCredentialsDto UserCredentials) : IRequest;
