using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nucleus.Application.Users;
using Nucleus.Application.Users.Dto;
using Nucleus.Core.Permissions;
using Nucleus.Utilities.Collections;
using Nucleus.Web.Core.Controllers;

namespace Nucleus.Web.Api.Controller.Users
{
    public class UserController : AdminController
    {
        private readonly IUserAppService _userAppService;

        public UserController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        [HttpGet("[action]")]
        [Authorize(Policy = DefaultPermissions.PermissionNameForUserRead)]
        public async Task<ActionResult<IPagedList<UserListOutput>>> GetUsers(UserListInput input)
        {
            return Ok(await _userAppService.GetUsersAsync(input));
        }

        [HttpDelete("[action]")]
        [Authorize(Policy = DefaultPermissions.PermissionNameForUserDelete)] 
        public ActionResult DeleteUser(Guid id)
        {
            _userAppService.RemoveUser(id);

            //todo: no need to return success true, OK is already ok.
            return Ok(new { success = true });
        }
    }
}