using AutoMapper;
using Nucleus.Application.Permissions.Dto;
using Nucleus.Application.Roles.Dto;
using Nucleus.Application.Users.Dto;
using Nucleus.Core.Permissions;
using Nucleus.Core.Roles;
using Nucleus.Core.Users;

namespace Nucleus.Application
{
    public class ApplicationServiceAutoMapperProfile : Profile
    {
        public ApplicationServiceAutoMapperProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(u => u.Password, opt => opt.Ignore());
            
            CreateMap<User, UserListOutput>();
            CreateMap<Permission, PermissionDto>();
            CreateMap<Role, RoleDto>();
            CreateMap<Role, RoleListOutput>();
        }
    }
}