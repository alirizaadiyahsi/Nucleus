using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nucleus.Application.Roles;
using Nucleus.Application.Roles.Dto;
using Nucleus.Utilities.Collections;
using Nucleus.Web.Core.Controllers;

namespace Nucleus.Web.Api.Controller.Roles
{
    //todo: add test cases for actions
    public class RoleController : AdminController
    {
        private readonly IRoleAppService _roleAppService;

        public RoleController(IRoleAppService roleAppService)
        {
            _roleAppService = roleAppService;
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
    }
}