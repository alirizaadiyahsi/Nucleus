using Microsoft.AspNetCore.Mvc;

namespace Nucleus.Web.Core.Controllers;

[ApiController]
[Route("api/[module]/[controller]")]
[Produces("application/json")]
public class ApiControllerBase : ControllerBase
{
}