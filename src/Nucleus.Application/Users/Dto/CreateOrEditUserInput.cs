using System.Collections.Generic;
using Nucleus.Application.Dto;
using Nucleus.Application.Permissions.Dto;

namespace Nucleus.Application.Users.Dto
{
    public class CreateOrEditUserInput : EntityDto
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public List<PermissionDto> Permissions { get; set; } = new List<PermissionDto>();
    }
}