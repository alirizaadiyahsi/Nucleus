using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nucleus.Application.Authorization.Users;
using Nucleus.Application.Authorization.Users.Dto;
using Nucleus.Application.Dto;
using Nucleus.Application.Identity;
using Nucleus.Domain.AppConstants;
using Nucleus.Utilities.Collections;
using Nucleus.Web.Core;
using Nucleus.Web.Core.Controllers;

namespace Nucleus.Modules.Admin.Controllers;

public class UsersController : AdminController
{
    private readonly IUserAppService _userAppService;
    private readonly IIdentityAppService _identityAppService;

    public UsersController(IUserAppService userAppService, IIdentityAppService identityAppService)
    {
        _userAppService = userAppService;
        _identityAppService = identityAppService;
    }

    [HttpGet("{id}")]
    [Authorize(AppPermissions.Users.Read)]
    public async Task<ActionResult<UserOutput>> GetUsers(Guid id)
    {
        var user = await _userAppService.GetAsync(id);
        if (user == null) return NotFound(UserFriendlyMessages.EntityNotFound);

        return Ok(user);
    }

    [HttpGet]
    [Authorize(AppPermissions.Users.Read)]
    public async Task<ActionResult<IPagedListResult<UserListOutput>>> GetUsers([FromQuery]PagedListInput input)
    {
        var users = await _userAppService.GetListAsync(input);

        return Ok(users);
    }

    [HttpPost]
    [Authorize(AppPermissions.Users.Create)]
    public async Task<ActionResult<UserOutput>> PostUsers([FromBody]CreateUserInput input)
    {
        var user = await _identityAppService.FindUserByEmailAsync(input.Email);
        if (user != null) return Conflict(UserFriendlyMessages.EmailAlreadyExist);

        user = await _identityAppService.FindUserByUserNameAsync(input.UserName);
        if (user != null) return Conflict(UserFriendlyMessages.UserNameAlreadyExist);

        var userOutput = await _userAppService.CreateAsync(input);

        return Ok(userOutput);
    }

    [HttpPut]
    [Authorize(AppPermissions.Users.Update)]
    public async Task<ActionResult<UserOutput>> PutUsers([FromBody]UpdateUserInput input)
    {
        var user = await _identityAppService.FindUserByEmailAsync(input.Email);
        if (user == null)
        {
            user = await _identityAppService.FindUserByUserNameAsync(input.UserName);
            if (user == null)
            {
                return NotFound(UserFriendlyMessages.UserIsNotFound);
            }
        }

        var userOutput = _userAppService.Update(input);

        return Ok(userOutput);
    }

    [HttpDelete("{id}")]
    [Authorize(AppPermissions.Users.Delete)]
    public async Task<ActionResult<UserOutput>> DeleteUsers(Guid id)
    {
        var userOutput = await _userAppService.DeleteAsync(id);

        return Ok(userOutput);
    }
}