using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Nucleus.Application.Users.Dto;
using Nucleus.Utilities.Collections;

namespace Nucleus.Application.Users
{
    public interface IUserAppService
    {
        Task<IPagedList<UserListOutput>> GetUsersAsync(UserListInput input);
        
        Task<IdentityResult> RemoveUserAsync(Guid id);
    }
}