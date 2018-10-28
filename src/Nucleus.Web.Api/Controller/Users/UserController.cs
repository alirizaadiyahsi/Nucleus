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
        [Authorize(Policy = DefaultPermissions.PermissionNameForUserList)]
        public async Task<ActionResult<IPagedList<UserListOutput>>> GetUsers(UserListInput input)
        {
            return Ok(await _userAppService.GetUsersAsync(input));
        }

        [HttpPost("[action]")]
        //[Authorize(Policy = DefaultPermissions.PermissionNameForUserAdd)] 
        public async Task<ActionResult> AddUser([FromBody]CreateOrEditUserInput input)
        {
            await _userAppService.AddUserAsync(input);

            return Ok(new { success = true });
        }
    }
}