using Microsoft.AspNetCore.Authorization;
using Nucleus.Core.Permissions;

namespace Nucleus.Web.Core.Controllers
{
    [Authorize(Policy = DefaultPermissions.PermissionNameForAdministration)]
    public class AdminController : BaseController
    {

    }
}