using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nucleus.Application.Permissions;
using Nucleus.Application.Roles;
using Nucleus.Application.Roles.Dto;
using Nucleus.Core.Permissions;
using Nucleus.Utilities.Collections;
using Nucleus.Web.Core.Controllers;

namespace Nucleus.Web.Api.Controller.Roles
{
    public class RoleController : AdminController
    {
        private readonly IRoleAppService _roleAppService;
        private readonly IPermissionAppService _permissionAppService;

        public RoleController(IRoleAppService roleAppService, IPermissionAppService permissionAppService)
        {
            _roleAppService = roleAppService;
            _permissionAppService = permissionAppService;
        }

        [HttpGet("[action]")]
        //todo: comment out this line after auto initialize permissions imlementation
        [Authorize(Policy = DefaultPermissions.PermissionNameForRoleRead)] 
        public async Task<ActionResult<IPagedList<RoleListOutput>>> GetRoles(RoleListInput input)
        {
            return Ok(await _roleAppService.GetRolesAsync(input));
        }

        [HttpPost("[action]")]
        //todo: comment out this line after auto initialize permissions imlementation
        [Authorize(Policy = DefaultPermissions.PermissionNameForRoleCreate)] 
        public async Task<ActionResult> CreateRole([FromBody]CreateOrEditRoleInput input)
        {
            await _roleAppService.AddRoleAsync(input);

            return Ok(new { success = true });
        }

        [HttpDelete("[action]")]
        //todo: comment out this line after auto initialize permissions imlementation
        [Authorize(Policy = DefaultPermissions.PermissionNameForRoleDelete)] 
        public ActionResult DeleteRole(Guid id)
        {
            _roleAppService.RemoveRole(id);

            return Ok(new { success = true });
        }

        // todo: create a GetRoleToCreateOrEdit model and move this logic to that class
        [HttpGet("[action]")]
        //todo: comment out this line after auto initialize permissions imlementation
        public ActionResult<IPagedList<RoleListOutput>> GetAllPermissions()
        {
            return Ok(_permissionAppService.GetAllPermissions());
        }
    }
}