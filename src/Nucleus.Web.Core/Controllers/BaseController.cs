using Microsoft.AspNetCore.Mvc;

namespace Nucleus.Web.Core.Controllers
{
    /// <inheritdoc />
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class BaseController : Controller
    {
    }
}
