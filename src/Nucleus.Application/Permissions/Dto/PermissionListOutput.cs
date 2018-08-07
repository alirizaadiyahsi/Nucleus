using Nucleus.Application.Dto;

namespace Nucleus.Application.Permissions.Dto
{
    public class PermissionListOutput:PagedListOutput
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }
    }
}