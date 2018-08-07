using Nucleus.Application.Dto;

namespace Nucleus.Application.Users.Dto
{
    public class UserListInput : PagedListInput
    {
        public UserListInput()
        {
            Sorting = "UserName";
        }
    }
}