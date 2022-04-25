using MediatR;
using UseCases.Common.User;

namespace UseCases.Users;

/// <summary>
/// Update password command.
/// </summary>
/// <param name="Dto">DTO.</param>
public record UpdatePasswordCommand(UpdatePasswordDto Dto) : IRequest;