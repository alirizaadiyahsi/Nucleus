using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Nucleus.Application.Permissions;
using Nucleus.Core.Permissions;
using Nucleus.Core.Roles;
using Nucleus.Core.Users;
using Nucleus.EntityFramework;
using Nucleus.Web.Core.Authentication;
using Xunit;

namespace Nucleus.Tests.Web.Api.RequirementHandlers
{
  public  class PermissionHandlerTest:ApiTestBase
    {
        [Fact]
        public async Task TestPermissionHandler()
        {
            var dbContext = TestServer.Host.Services.GetRequiredService<NucleusDbContext>();
            var mapper = TestServer.Host.Services.GetRequiredService<IMapper>();
            var permissionAppService = new PermissionAppService(dbContext, mapper);

            var requirements = new List<PermissionRequirement>
            {
                new PermissionRequirement(DefaultPermissions.MemberAccess)
            };
            var authorizationHandlerContext = new AuthorizationHandlerContext(requirements, ContextUser, null);
            var permissionHandler = new PermissionHandler(permissionAppService);
            await permissionHandler.HandleAsync(authorizationHandlerContext);

            Assert.True(authorizationHandlerContext.HasSucceeded);
        }
    }
}
