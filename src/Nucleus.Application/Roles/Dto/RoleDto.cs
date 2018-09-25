using System.Collections.Generic;
using Nucleus.Application.Dto;
using Nucleus.Application.Permissions.Dto;

namespace Nucleus.Application.Roles.Dto
{
    public class RoleDto : EntityDto
    {
        public string Name { get; set; }

        public List<PermissionDto> Permissions { get; set; } = new List<PermissionDto>();
    }
}
