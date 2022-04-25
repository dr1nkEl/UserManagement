using MediatR;
using UseCases.Common.User;

namespace UseCases.Users;

/// <summary>
/// Update login command.
/// </summary>
/// <param name="Dto">DTO.</param>
public record UpdateLoginCommand(UpdateLoginDto Dto) : IRequest;
