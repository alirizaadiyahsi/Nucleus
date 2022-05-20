using Microsoft.AspNetCore.Authorization;

namespace Nucleus.Web.Core.Controllers;

[Authorize]
public class AuthorizedController : ApiControllerBase
{
}