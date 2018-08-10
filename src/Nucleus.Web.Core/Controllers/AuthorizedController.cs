using Microsoft.AspNetCore.Authorization;
using Nucleus.Core.Permissions;

namespace Nucleus.Web.Core.Controllers
{
    [Authorize(Policy = DefaultPermissions.PermissionNameForMemberAccess)]
    public class AuthorizedController : BaseController
    {

    }
}