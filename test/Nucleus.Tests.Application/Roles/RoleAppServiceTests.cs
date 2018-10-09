using System;
using System.Collections.Generic;
using System.Linq;
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
        public async void Should_Add_Remove_Role()
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

            _roleAppService.RemoveRole(insertedTestRole.Id);
            _dbContext.SaveChanges();

            dbContextFromAnotherScope = TestServer.Host.Services.GetRequiredService<NucleusDbContext>();
            var removedTestRole = await dbContextFromAnotherScope.Roles.FindAsync(testRole.Id);
            var removedPermissionMatches = dbContextFromAnotherScope.RolePermissions.Where(rp => rp.RoleId == testRole.Id);
            var removedUserMatches = dbContextFromAnotherScope.RolePermissions.Where(rp => rp.RoleId == testRole.Id);

            Assert.Null(removedTestRole);
            Assert.Equal(0, removedPermissionMatches.Count());
            Assert.Equal(0, removedUserMatches.Count());

            //todo: seperate tests create and remove
        }

        [Fact]
        public async void Should_Get_Roles()
        {
            var roleListInput = new RoleListInput();
            var roleList = await _roleAppService.GetRolesAsync(roleListInput);
            Assert.True(roleList.Items.Count >= 0);

            roleListInput.Filter = ".!1Aa_";
            var rolesListEmpty = await _roleAppService.GetRolesAsync(roleListInput);
            Assert.True(rolesListEmpty.Items.Count == 0);
        }
    }
}
