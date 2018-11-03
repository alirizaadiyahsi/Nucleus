using Nucleus.Application.Dto;

namespace Nucleus.Application.Roles.Dto
{
    public class RoleListInput : PagedListInput
    {
        public RoleListInput()
        {
            SortBy = "Name";
        }
    }
}