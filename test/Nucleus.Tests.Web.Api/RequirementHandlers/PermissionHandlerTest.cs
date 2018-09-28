using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Nucleus.Application.Permissions;
using Nucleus.Core.Permissions;
using Nucleus.Web.Core.Authentication;
using Xunit;

namespace Nucleus.Tests.Web.Api.RequirementHandlers
{
    public class PermissionHandlerTest : ApiTestBase
    {
        [Fact]
        public async Task Should_Handle_Permission()
        {
            var permissionAppService = TestServer.Host.Services.GetRequiredService<IPermissionAppService>();

            var requirements = new List<PermissionRequirement>
            {
                new PermissionRequirement(DefaultPermissions.MemberAccess)
            };
            var authorizationHandlerContext = new AuthorizationHandlerContext(requirements, ContextUser, null);
            var permissionHandler = new PermissionHandler(permissionAppService);
            await permissionHandler.HandleAsync(authorizationHandlerContext);

            Assert.True(authorizationHandlerContext.HasSucceeded);
        }

        [Fact]
        public async Task Should_Not_Handle_Permission_With_Null_User()
        {
            var permissionAppService = TestServer.Host.Services.GetRequiredService<IPermissionAppService>();

            var requirements = new List<PermissionRequirement>
            {
                new PermissionRequirement(DefaultPermissions.MemberAccess)
            };
            var authorizationHandlerContext = new AuthorizationHandlerContext(requirements, null, null);
            var permissionHandler = new PermissionHandler(permissionAppService);
            await permissionHandler.HandleAsync(authorizationHandlerContext);

            Assert.True(authorizationHandlerContext.HasFailed);
        }
    }
}
