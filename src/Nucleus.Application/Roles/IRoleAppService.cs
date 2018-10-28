using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Nucleus.Application.Roles.Dto;
using Nucleus.Utilities.Collections;

namespace Nucleus.Application.Roles
{
    public interface IRoleAppService
    {
        Task<IPagedList<RoleListOutput>> GetRolesAsync(RoleListInput input);

        Task<GetRoleForCreateOrUpdateOutput> GetRoleForCreateOrUpdateAsync(Guid id);

        Task<IdentityResult> AddRoleAsync(CreateOrUpdateRoleInput input);

        Task<IdentityResult> EditRoleAsync(CreateOrUpdateRoleInput input);

        Task<IdentityResult> RemoveRoleAsync(Guid id);
    }
}
