using API.Infrastructure.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UseCases.Common.Role;

namespace API.Infrastructure.MappingProfiles;

/// <summary>
/// Role mapping profile.
/// </summary>
public class RoleMappingProfile : Profile
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public RoleMappingProfile()
    {
        CreateMap<IdentityRole<int>, RoleDto>();
        CreateMap<AddUserToRole, AddUserToRoleDto>();
    }
}
