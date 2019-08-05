using Microsoft.AspNetCore.Mvc;
using Nucleus.Web.Core.Controllers;

namespace Nucleus.Web.Api.Controller.Test
{
    public class TestController : BaseController
    {
        [HttpGet("[action]")]
        public ObjectResult Get(string userNameOrEmail)
        {
            return Ok(new { asd = "Test success!" });
        }
    }
}