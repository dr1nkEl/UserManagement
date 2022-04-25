using API.Infrastructure.Models;
using AutoMapper;
using Domain;
using UseCases.Common.User;

namespace API.Infrastructure.MappingProfiles;

/// <summary>
/// User mapping profile.
/// </summary>
public class UserMappingProfile : Profile
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public UserMappingProfile()
    {
        CreateMap<User, UseCases.Common.User.UserDto>();
        CreateMap<UseCases.Common.User.UserDto, Models.UserDto>();
        CreateMap<UserCredentials, UserCredentialsDto>();
        CreateMap<UserRegister, UserRegisterDto>();
        CreateMap<PatchUser, PatchUserDto>();
        CreateMap<UpdateLogin, UpdateLoginDto>();
        CreateMap<UpdatePassword, UpdatePasswordDto>();
    }
}
