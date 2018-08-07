using Nucleus.Application.Dto;

namespace Nucleus.Application.Permissions.Dto
{
    public class PermissionListInput : PagedListInput
    {
        public PermissionListInput()
        {
            Sorting = "Name";
        }
    }
}