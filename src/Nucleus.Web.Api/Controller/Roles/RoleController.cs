using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nucleus.Application.Permissions;
using Nucleus.Application.Roles;
using Nucleus.Application.Roles.Dto;
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
        //[Authorize(Policy = DefaultPermissions.PermissionNameForRoleList)] 
        public async Task<ActionResult<IPagedList<RoleListOutput>>> GetRoles(RoleListInput input)
        {
            return Ok(await _roleAppService.GetRolesAsync(input));
        }

        [HttpPost("[action]")]
        //todo: comment out this line after auto initialize permissions imlementation
        //[Authorize(Policy = DefaultPermissions.PermissionNameForRoleAdd)] 
        public async Task<ActionResult> AddRole([FromBody]CreateOrEditRoleInput input)
        {
            await _roleAppService.AddRoleAsync(input);

            return Ok(new { success = true });
        }

        [HttpDelete("[action]")]
        //todo: comment out this line after auto initialize permissions imlementation
        //[Authorize(Policy = DefaultPermissions.PermissionNameForRoleDelete)] 
        public ActionResult RemoveRole(Guid id)
        {
            _roleAppService.RemoveRole(id);

            return Ok(new { success = true });
        }

        [HttpGet("[action]")]
        //todo: comment out this line after auto initialize permissions imlementation
        //[Authorize(Policy = DefaultPermissions.PermissionNameForRoleAdd)] 
        public ActionResult<IPagedList<RoleListOutput>> GetAllPermissions()
        {
            return Ok(_permissionAppService.GetAllPermissions());
        }
    }
}