using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Nucleus.Core.Roles;
using Nucleus.EntityFramework;
using Nucleus.Web.Core.ActionFilters;
using Xunit;

namespace Nucleus.Tests.Web.Api.ActionFilters
{
    public class UnitOfWorkFilterTest : ApiTestBase
    {
        [Fact]
        public void TestUnitOfWorkActionFilter()
        {
            var dbContext = TestServer.Host.Services.GetRequiredService<NucleusDbContext>();
            var unitOfWorkActionFilter = new UnitOfWorkActionFilter(dbContext);
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
            var testRole = new Role { Name = "test_role" };

            dbContext.Roles.Add(testRole);

            //created a new db context to get non-local(non-tracking) data
            var dbContextForGet = TestServer.Host.Services.GetRequiredService<NucleusDbContext>();
            var insertedTestRole = dbContextForGet.Roles.Find(testRole.Id);
            Assert.Null(insertedTestRole);

            unitOfWorkActionFilter.OnActionExecuted(actionExecutedContext);

            insertedTestRole = dbContextForGet.Roles.Find(testRole.Id);
            Assert.NotNull(insertedTestRole);
        }
    }
}
