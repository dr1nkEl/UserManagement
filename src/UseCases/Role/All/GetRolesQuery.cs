using MediatR;
using UseCases.Common.Role;

namespace UseCases.Role;

/// <summary>
/// Get roles query.
/// </summary>
public record GetRolesQuery : IRequest<IEnumerable<RoleDto>>;
