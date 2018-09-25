using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Nucleus.Application.Permissions.Dto;
using Nucleus.Application.Roles;
using Nucleus.Application.Roles.Dto;
using Nucleus.Core.Permissions;
using Nucleus.EntityFramework;
using Nucleus.Web.Core.ActionFilters;
using Xunit;

namespace Nucleus.Tests.Web.Api.ActionFilters
{
    public class UnitOfWorkFilterTest : ApiTestBase
    {
        private readonly NucleusDbContext _dbContext;
        private readonly IRoleAppService _roleAppService;

        public UnitOfWorkFilterTest()
        {
            var serviceProvider = TestServer.Host.Services.CreateScope().ServiceProvider;
            _dbContext = serviceProvider.GetRequiredService<NucleusDbContext>();
            _roleAppService = serviceProvider.GetRequiredService<IRoleAppService>();
        }

        [Fact]
        public async Task TestUnitOfWorkActionFilter()
        {
            var testRole = new RoleDto
            {
                Id = Guid.NewGuid(),
                Name = "TestRole_" + Guid.NewGuid(),
                Permissions = new List<PermissionDto>
                {
                    new PermissionDto
                    {
                        Id =  DefaultPermissions.MemberAccess.Id,
                        Name = DefaultPermissions.MemberAccess.Name,
                        DisplayName = DefaultPermissions.MemberAccess.DisplayName
                    }
                }
            };

            var unitOfWorkActionFilter = new UnitOfWorkActionFilter(_dbContext);
            var actionContext = new ActionContext(
                new DefaultHttpContext
                {
                    Request =
                    {
                            Method = "Post"
                    }
                },
                new RouteData(),
                new ActionDescriptor()
            );

            var actionExecutedContext = new ActionExecutedContext(actionContext, new List<IFilterMetadata>(), null);
            await _roleAppService.AddRoleAsync(testRole);

            unitOfWorkActionFilter.OnActionExecuted(actionExecutedContext);

            var dbContextFromAnotherScope = TestServer.Host.Services.GetRequiredService<NucleusDbContext>();
            var insertedTestRole = await dbContextFromAnotherScope.Roles.FindAsync(testRole.Id);
            Assert.Equal(1, insertedTestRole.RolePermissions.Count);
        }
    }
}
