using Nucleus.Application.Dto;

namespace Nucleus.Application.Roles.Dto
{
    public class RoleDto : EntityDto
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }
    }
}