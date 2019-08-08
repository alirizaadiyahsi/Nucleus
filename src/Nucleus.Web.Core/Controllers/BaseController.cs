using Microsoft.AspNetCore.Mvc;

namespace Nucleus.Web.Core.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class BaseController : Controller
    {
        // TODO: Update derived controller's action to return correct HTTP result
        // https://hackernoon.com/restful-api-designing-guidelines-the-best-practices-60e1d954e7c9
    }
}
