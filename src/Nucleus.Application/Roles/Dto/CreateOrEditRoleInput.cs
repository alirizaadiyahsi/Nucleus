using System;
using Nucleus.Application.Dto;

namespace Nucleus.Application.Roles.Dto
{
    public class CreateOrEditRoleInput : EntityDto
    {
        public string Name { get; set; }

        public Guid[] PermissionIds { get; set; }
    }
}
