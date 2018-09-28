using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Nucleus.Core.Roles;
using Nucleus.EntityFramework;
using Nucleus.Web.Core.ActionFilters;
using Xunit;

namespace Nucleus.Tests.Web.Api.ActionFilters
{
    public class UnitOfWorkFilterTest : ApiTestBase
    {
        private readonly NucleusDbContext _dbContext;

        public UnitOfWorkFilterTest()
        {
            _dbContext = TestServer.Host.Services.GetRequiredService<NucleusDbContext>();
        }

        [Fact]
        public async Task Should_UnitOfWork_Action_Filter_Save_Changes()
        {
            var testRole = new Role
            {
                Id = Guid.NewGuid(),
                Name = "TestRole_" + Guid.NewGuid()
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
            await _dbContext.Roles.AddAsync(testRole);

            var dbContextFromAnotherScope = TestServer.Host.Services.GetRequiredService<NucleusDbContext>();
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

            var unitOfWorkActionFilter = new UnitOfWorkActionFilter(_dbContext);
            var actionContext = new ActionContext(
                new DefaultHttpContext(),
                new RouteData(),
                new ActionDescriptor()
            );

            var actionExecutedContext = new ActionExecutedContext(actionContext, new List<IFilterMetadata>(), null);
            await _dbContext.Roles.AddAsync(testRole);

            var dbContextFromAnotherScope = TestServer.Host.Services.GetRequiredService<NucleusDbContext>();
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
                Id = Guid.NewGuid(),
                Name = "TestRole_" + new string('*', 300)//allowed max role name length is 256
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
            await _dbContext.Roles.AddAsync(testRole);

            var exception = Assert.Throws<DbUpdateException>(() => unitOfWorkActionFilter.OnActionExecuted(actionExecutedContext));
            Assert.Equal(typeof(DbUpdateException), exception.GetType());
        }
    }
}
