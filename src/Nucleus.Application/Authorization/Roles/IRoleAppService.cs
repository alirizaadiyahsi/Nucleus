using Nucleus.Application.Authorization.Roles.Dto;
using Nucleus.Application.Crud;
using Nucleus.Domain.Entities.Authorization;

namespace Nucleus.Application.Authorization.Roles;

public interface IRoleAppService: ICrudAppService<Role, RoleOutput, RoleListOutput, CreateRoleInput, UpdateRoleInput>
{
}