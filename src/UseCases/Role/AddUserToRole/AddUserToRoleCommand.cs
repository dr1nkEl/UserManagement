using MediatR;
using UseCases.Common.Role;

namespace UseCases.Role;

/// <summary>
/// Add user to role command.
/// </summary>
/// <param name="Dto">DTO.</param>
public record AddUserToRoleCommand(AddUserToRoleDto Dto) : IRequest;