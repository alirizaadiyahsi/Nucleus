using System.Threading.Tasks;
using Nucleus.Application.Roles.Dto;

namespace Nucleus.Application.Roles
{
    public interface IRoleAppService
    {
        Task AddRoleAsync(RoleDto roleDto);
    }
}
