using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nucleus.Application.Authorization.Roles;
using Nucleus.Application.Authorization.Roles.Dto;
using Nucleus.Application.Dto;
using Nucleus.Application.Identity;
using Nucleus.Domain.AppConstants;
using Nucleus.Utilities.Collections;
using Nucleus.Web.Core;
using Nucleus.Web.Core.Controllers;

namespace Nucleus.Modules.Admin.Controllers;

public class RolesController: AdminController
{
    private readonly IRoleAppService _roleAppService;
    private readonly IIdentityAppService _identityAppService;

    public RolesController(IRoleAppService roleAppService, IIdentityAppService identityAppService)
    {
        _roleAppService = roleAppService;
        _identityAppService = identityAppService;
    }

    [HttpGet("{id}")]
    [Authorize(AppPermissions.Roles.Read)]
    public async Task<ActionResult<RoleOutput>> GetRoles(Guid id)
    {
        var role = await _roleAppService.GetAsync(id);
        if (role == null) return NotFound(UserFriendlyMessages.EntityNotFound);

        return Ok(role);
    }

    [HttpGet]
    [Authorize(AppPermissions.Roles.Read)]
    public async Task<ActionResult<IPagedListResult<RoleListOutput>>> GetRoles([FromQuery]PagedListInput input)
    {
        var roles = await _roleAppService.GetListAsync(input);

        return Ok(roles);
    }

    [HttpPost]
    [Authorize(AppPermissions.Roles.Create)]
    public async Task<ActionResult<RoleOutput>> PostRoles([FromBody]CreateRoleInput input)
    {
        var role = await _identityAppService.FindRoleByNameAsync(input.Name);
        if (role != null) return Conflict(UserFriendlyMessages.RoleNameAlreadyExist);

        var roleOutput = await _roleAppService.CreateAsync(input);

        return Ok(roleOutput);
    }

    [HttpPut]
    [Authorize(AppPermissions.Roles.Update)]
    public async Task<ActionResult<RoleOutput>> PutRoles([FromBody]UpdateRoleInput input)
    {
        var role = await _identityAppService.FindRoleByNameAsync(input.Name);
        if (role == null) return NotFound(UserFriendlyMessages.RoleNotFount);

        var roleOutput = _roleAppService.Update(input);

        return Ok(roleOutput);
    }

    [HttpDelete("{id}")]
    [Authorize(AppPermissions.Roles.Delete)]
    public async Task<ActionResult<RoleOutput>> DeleteRoles(Guid id)
    {
        var roleOutput = await _roleAppService.DeleteAsync(id);

        return Ok(roleOutput);
    }
}