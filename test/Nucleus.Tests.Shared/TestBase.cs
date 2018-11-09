using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Nucleus.Core.Users;
using Nucleus.Web.Api;

namespace Nucleus.Tests.Shared
{
    public class TestBase
    {
        protected ClaimsPrincipal ContextUser => new ClaimsPrincipal(
            new ClaimsIdentity(
                new List<Claim>
                {
                    new Claim(ClaimTypes.Name,  DefaultUsers.Member.UserName)
                },
                "TestAuthenticationType"
            )
        );

        protected TestServer TestServer => new TestServer(
            new WebHostBuilder()
                .UseStartup<Startup>()
                .UseEnvironment("Test")
                .ConfigureAppConfiguration(config =>
                {
                    config.SetBasePath(Path.Combine(Path.GetFullPath(@"../../../.."), "Nucleus.Tests.Shared"));
                    config.AddJsonFile("appsettings.json", false);
                })
        );
    }
}
