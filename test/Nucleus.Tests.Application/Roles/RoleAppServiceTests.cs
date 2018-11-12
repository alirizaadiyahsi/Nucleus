using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Nucleus.Application.Roles;
using Nucleus.Application.Roles.Dto;
using Nucleus.Core.Permissions;
using Nucleus.Core.Roles;
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
            var input = new CreateOrUpdateRoleInput
            {
                Role = new RoleDto
                {
                    Id = Guid.NewGuid(),
                    Name = "TestRoleName_" + Guid.NewGuid()
                },
                GrantedPermissionIds = new List<Guid> { DefaultPermissions.MemberAccess.Id }
            };

            await _roleAppService.AddRoleAsync(input);
            await _dbContext.SaveChangesAsync();

            var dbContextFromAnotherScope = TestServer.Host.Services.GetRequiredService<NucleusDbContext>();
            var insertedTestRole = await dbContextFromAnotherScope.Roles.FindAsync(input.Role.Id);

            Assert.NotNull(insertedTestRole);
            Assert.Equal(1, insertedTestRole.RolePermissions.Count);
        }

        [Fact]
        public async void Should_Edit_Role()
        {
            var testRole = await CreateAndGetTestRoleAsync();

            var input = new CreateOrUpdateRoleInput
            {
                Role = new RoleDto
                {
                    Id = testRole.Id,
                    Name = "TestRoleName_Edited_" + Guid.NewGuid()
                },
                GrantedPermissionIds = new List<Guid> { DefaultPermissions.MemberAccess.Id }
            };
            await _roleAppService.EditRoleAsync(input);
            var editedTestRole = await _dbContext.Roles.FindAsync(testRole.Id);

            Assert.Contains("TestRoleName_Edited_", editedTestRole.Name);
            Assert.Contains(editedTestRole.RolePermissions, rp => rp.PermissionId == DefaultPermissions.MemberAccess.Id);
        }

        [Fact]
        public async void Should_Remove_Role()
        {
            var testRole = await CreateAndGetTestRoleAsync();

            var dbContextFromAnotherScope = TestServer.Host.Services.GetRequiredService<NucleusDbContext>();
            var insertedTestRole = await dbContextFromAnotherScope.Roles.FindAsync(testRole.Id);

            Assert.NotNull(insertedTestRole);
            Assert.Equal(1, insertedTestRole.RolePermissions.Count);

            await _roleAppService.RemoveRoleAsync(insertedTestRole.Id);

            dbContextFromAnotherScope = TestServer.Host.Services.GetRequiredService<NucleusDbContext>();
            var removedTestRole = await dbContextFromAnotherScope.Roles.FindAsync(testRole.Id);
            var removedPermissionMatches = dbContextFromAnotherScope.RolePermissions.Where(rp => rp.RoleId == testRole.Id);
            var removedUserMatches = dbContextFromAnotherScope.RolePermissions.Where(rp => rp.RoleId == testRole.Id);

            Assert.Null(removedTestRole);
            Assert.Equal(0, removedPermissionMatches.Count());
            Assert.Equal(0, removedUserMatches.Count());
        }

        [Fact]
        public async void Should_Get_Roles()
        {
            var roleListInput = new RoleListInput();
            var roleList = await _roleAppService.GetRolesAsync(roleListInput);
            Assert.True(roleList.Items.Count > 0);

            roleListInput.Filter = ".!1Aa_";
            var rolesListEmpty = await _roleAppService.GetRolesAsync(roleListInput);
            Assert.True(rolesListEmpty.Items.Count == 0);
        }

        [Fact]
        public async void Should_Get_Role_For_Create()
        {
            var role = await _roleAppService.GetRoleForCreateOrUpdateAsync(Guid.Empty);
            Assert.True(string.IsNullOrEmpty(role.Role.Name));
        }

        [Fact]
        public async void Should_Get_Role_For_Update()
        {
            var role = await _roleAppService.GetRoleForCreateOrUpdateAsync(DefaultRoles.Member.Id);
            Assert.False(string.IsNullOrEmpty(role.Role.Name));
        }

        private async Task<Role> CreateAndGetTestRoleAsync()
        {
            var testRole = new Role { Id = Guid.NewGuid(), Name = "TestRoleName_" + Guid.NewGuid() };
            await _dbContext.Roles.AddAsync(testRole);
            await _dbContext.RolePermissions.AddAsync(new RolePermission
            {
                RoleId = testRole.Id,
                PermissionId = DefaultPermissions.AdministrationAccess.Id
            });
            await _dbContext.SaveChangesAsync();
            return testRole;
        }
    }
}
