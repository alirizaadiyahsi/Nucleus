using System;
using System.Collections.Generic;
using Nucleus.Application.Roles.Dto;

namespace Nucleus.Application.Users.Dto
{
    public class GetUserForCreateOrUpdateOutput
    {
        public UserDto User { get; set; } = new UserDto();

        public List<RoleDto> AllRoles { get; set; } = new List<RoleDto>();

        public List<Guid> GrantedRoleIds { get; set; } = new List<Guid>();
    }
}