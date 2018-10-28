using System.Threading.Tasks;
using Nucleus.Application.Users.Dto;
using Nucleus.Utilities.Collections;

namespace Nucleus.Application.Users
{
    public interface IUserAppService
    {
        Task AddUserAsync(CreateOrEditUserInput input);

        Task<IPagedList<UserListOutput>> GetUsersAsync(UserListInput input);
    }
}