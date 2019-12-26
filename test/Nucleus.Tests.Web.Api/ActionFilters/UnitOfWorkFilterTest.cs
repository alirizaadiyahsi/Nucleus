using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        public async Task Should_UnitOfWork_Action_Filter_Save_Changes()
        {
            var testRole = new Role
            {
                Id = Guid.NewGuid(),
                Name = "TestRole_" + Guid.NewGuid()
            };

            var unitOfWorkActionFilter = new UnitOfWorkActionFilter(DbContext);
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
            await DbContext.Roles.AddAsync(testRole);

            var dbContextFromAnotherScope = GetNewScopeServiceProvider().GetService<NucleusDbContext>();
            var insertedTestRole = await dbContextFromAnotherScope.Roles.FindAsync(testRole.Id);
            Assert.Null(insertedTestRole);

            unitOfWorkActionFilter.OnActionExecuted(actionExecutedContext);

            insertedTestRole = await dbContextFromAnotherScope.Roles.FindAsync(testRole.Id);
            Assert.NotNull(insertedTestRole);
        }

        [Fact]
        public async Task Should_Not_UnitOfWork_Action_Filter_Save_Changes_Except_Post()
        {
            var testRole = new Role
            {
                Id = Guid.NewGuid(),
                Name = "TestRole_" + Guid.NewGuid()
            };

            var unitOfWorkActionFilter = new UnitOfWorkActionFilter(DbContext);
            var actionContext = new ActionContext(
                new DefaultHttpContext(),
                new RouteData(),
                new ActionDescriptor()
            );

            var actionExecutedContext = new ActionExecutedContext(actionContext, new List<IFilterMetadata>(), null);
            await DbContext.Roles.AddAsync(testRole);

            var dbContextFromAnotherScope = GetNewScopeServiceProvider().GetService<NucleusDbContext>();
            var insertedTestRole = await dbContextFromAnotherScope.Roles.FindAsync(testRole.Id);
            Assert.Null(insertedTestRole);

            unitOfWorkActionFilter.OnActionExecuted(actionExecutedContext);

            insertedTestRole = await dbContextFromAnotherScope.Roles.FindAsync(testRole.Id);
            Assert.Null(insertedTestRole);
        }

        [Fact]
        public async Task Should_Not_UnitOfWork_Action_Filter_Save_Changes_OnException()
        {
            var testRole = new Role
            {
                Id = DefaultRoles.Admin.Id // set same id with admin role
            };

            var unitOfWorkActionFilter = new UnitOfWorkActionFilter(DbContext);
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
            await DbContext.Roles.AddAsync(testRole);

            var exception = Assert.Throws<ArgumentException>(() => unitOfWorkActionFilter.OnActionExecuted(actionExecutedContext));
            Assert.Equal(typeof(ArgumentException), exception.GetType());
        }
    }
}
