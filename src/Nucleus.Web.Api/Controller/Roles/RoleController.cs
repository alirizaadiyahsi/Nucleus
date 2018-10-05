using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nucleus.Application.Roles;
using Nucleus.Application.Roles.Dto;
using Nucleus.Application.Users;
using Nucleus.Application.Users.Dto;
using Nucleus.Core.Permissions;
using Nucleus.Utilities.Collections;
using Nucleus.Web.Core.Controllers;

namespace Nucleus.Web.Api.Controller.Roles
{
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
    }
}