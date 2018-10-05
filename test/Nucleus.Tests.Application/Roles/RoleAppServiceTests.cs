using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Nucleus.Application.Permissions.Dto;
using Nucleus.Application.Roles;
using Nucleus.Application.Roles.Dto;
using Nucleus.Core.Permissions;
using Nucleus.EntityFramework;
using Xunit;

namespace Nucleus.Tests.Application.Roles
{
    public class RoleAppServiceTests : ApplicationTestBase
    {
        private readonly NucleusDbContext _dbContext;
        private readonly IRoleAppService _roleAppService;

        public RoleAppServiceTests()
        {
            var serviceProvider = TestServer.Host.Services.CreateScope().ServiceProvider;
            _dbContext = serviceProvider.GetRequiredService<NucleusDbContext>();
            _roleAppService = serviceProvider.GetRequiredService<IRoleAppService>();
        }

        [Fact]
        public async void Should_Add_Role()
        {
            var testRole = new CreateOrEditRoleInput
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

            await _roleAppService.AddRoleAsync(testRole);
            await _dbContext.SaveChangesAsync();

            var dbContextFromAnotherScope = TestServer.Host.Services.GetRequiredService<NucleusDbContext>();
            var insertedTestRole = await dbContextFromAnotherScope.Roles.FindAsync(testRole.Id);

            Assert.NotNull(insertedTestRole);
            Assert.Equal(1, insertedTestRole.RolePermissions.Count);
        }
    }
}
