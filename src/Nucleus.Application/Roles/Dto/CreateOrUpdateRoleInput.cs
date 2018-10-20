using System;
using System.Collections.Generic;
using Nucleus.Application.Dto;

namespace Nucleus.Application.Roles.Dto
{
    public class CreateOrUpdateRoleInput
    {
        public RoleDto Role { get; set; } = new RoleDto();

        public List<Guid> GrantedPermissionIds { get; set; } = new List<Guid>();
    }
}
