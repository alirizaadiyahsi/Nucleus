using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nucleus.Application.Users;
using Nucleus.Application.Users.Dto;
using Nucleus.Core.Permissions;
using Nucleus.Core.Users;
using Nucleus.Utilities.Collections;
using Nucleus.Web.Core.Controllers;

namespace Nucleus.Web.Api.Controllers
{
    public class TestController : BaseController
    {
        private readonly IUserAppService _userAppService;
        private static readonly string[] Summaries = {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public TestController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        [HttpGet("[action]")]
        [Authorize(Policy = DefaultPermissions.PermissionNameForMemberAccess)]
        public async Task<ActionResult<IPagedList<UserListOutput>>> Users()
        {
            return Ok(await _userAppService.GetUsersAsync(new UserListInput()));
        }

        //todo: return user dto instead of domain object
        [HttpGet("[action]/{username}")]
        [Authorize(Policy = DefaultPermissions.PermissionNameForMemberAccess)]
        public ActionResult<User> Users(string userName)
        {
            return Ok(new User
            {
                Id = Guid.NewGuid(),
                UserName = userName
            });
        }
    }
}