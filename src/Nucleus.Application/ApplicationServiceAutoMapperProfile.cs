using AutoMapper;
using Nucleus.Application.Authorization.Roles.Dto;
using Nucleus.Application.Authorization.Users.Dto;
using Nucleus.Domain.AppConstants;
using Nucleus.Domain.Entities.Authorization;

namespace Nucleus.Application;

public class ApplicationServiceAutoMapperProfile : Profile
{
    public ApplicationServiceAutoMapperProfile()
    {
        CreateMap<User, UserOutput>()
            .ForMember(dest => dest.SelectedRoleIds,
                opt => opt.MapFrom(src => src.UserRoles.Select(x => x.RoleId)))
            .ForMember(dest => dest.SelectedPermissions,
                opt => opt.MapFrom(src => src.UserClaims.Where(uc => uc.ClaimType == AppClaimTypes.Permission).Select(uc => uc.ClaimValue)));
        CreateMap<CreateUserInput, User>();
        CreateMap<UpdateUserInput, User>();
        CreateMap<UserOutput, User>();
        CreateMap<User, UserListOutput>();

        CreateMap<Role, RoleOutput>()
            .ForMember(dest => dest.SelectedPermissions,
                opt => opt.MapFrom(src => src.RoleClaims.Where(uc => uc.ClaimType == AppClaimTypes.Permission).Select(uc => uc.ClaimValue)));
        CreateMap<CreateRoleInput, Role>();
        CreateMap<UpdateRoleInput, Role>();
        CreateMap<Role, RoleListOutput>();
    }
}