using System.Threading.Tasks;
using Nucleus.Application.Roles.Dto;
using Nucleus.Utilities.Collections;

namespace Nucleus.Application.Roles
{
    public interface IRoleAppService
    {
        Task AddRoleAsync(CreateOrEditRoleInput roleDto);

        Task<IPagedList<RoleListOutput>> GetRolesAsync(RoleListInput input);
    }
}
