using System;
using System.Threading.Tasks;
using Nucleus.Application.Roles.Dto;
using Nucleus.Utilities.Collections;

namespace Nucleus.Application.Roles
{
    public interface IRoleAppService
    {
        Task<IPagedList<RoleListOutput>> GetRolesAsync(RoleListInput input);

        Task<GetRoleForCreateOrUpdateOutput> GetRoleForCreateOrUpdateAsync(Guid id);

        Task AddRoleAsync(CreateOrUpdateRoleInput input);

        Task EditRoleAsync(CreateOrUpdateRoleInput input);

        void RemoveRole(Guid id);
    }
}
