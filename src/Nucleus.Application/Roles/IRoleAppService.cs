using System;
using System.Threading.Tasks;
using Nucleus.Application.Roles.Dto;
using Nucleus.Utilities.Collections;

namespace Nucleus.Application.Roles
{
    public interface IRoleAppService
    {
        Task AddRoleAsync(CreateOrUpdateRoleInput input);

        Task<IPagedList<RoleListOutput>> GetRolesAsync(RoleListInput input);

        void RemoveRole(Guid id);

        Task<GetRoleForCreateOrUpdateOutput> GetRoleForCreateOrUpdateAsync(GetRoleForCreateOrUpdateInput input);
    }
}
