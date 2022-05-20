using Microsoft.AspNetCore.Authorization;
using Nucleus.Domain.AppConstants;

namespace Nucleus.Web.Core.Controllers;

[Authorize(Policy = AppPermissions.Administration)]
public class AdminController : ApiControllerBase
{
}