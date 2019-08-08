using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Nucleus.Application.Roles.Dto;
using Nucleus.Core.Permissions;
using Nucleus.Core.Roles;
using Nucleus.EntityFramework;
using Nucleus.Utilities.Collections;
using Nucleus.Utilities.Extensions.PrimitiveTypes;
using Xunit;

namespace Nucleus.Tests.Web.Api.Controllers
{
    public class RoleControllerTests : ApiTestBase
    {
        private readonly NucleusDbContext _dbContext;
        private readonly string _token;

        public RoleControllerTests()
        {
            _dbContext = TestServer.Host.Services.GetRequiredService<NucleusDbContext>();
            _token = LoginAsAdminUserAndGetTokenAsync().Result;
        }

        [Fact]
        public async Task Should_Get_Roles()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/api/roles");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var responseGetRoles = await TestServer.CreateClient().SendAsync(requestMessage);
            Assert.Equal(HttpStatusCode.OK, responseGetRoles.StatusCode);

            var roles = await responseGetRoles.Content.ReadAsAsync<PagedList<RoleListOutput>>();
            Assert.True(roles.Items.Any());
        }

        [Fact]
        public async Task Should_Get_Role_For_Create()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/api/roles/" + Guid.Empty);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var responseGetRoles = await TestServer.CreateClient().SendAsync(requestMessage);
            Assert.Equal(HttpStatusCode.OK, responseGetRoles.StatusCode);

            var role = await responseGetRoles.Content.ReadAsAsync<GetRoleForCreateOrUpdateOutput>();
            Assert.True(string.IsNullOrEmpty(role.Role.Name));
        }

        [Fact]
        public async Task Should_Get_Role_For_Update()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/api/roles/" + DefaultRoles.Member.Id);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var responseGetRoles = await TestServer.CreateClient().SendAsync(requestMessage);
            Assert.Equal(HttpStatusCode.OK, responseGetRoles.StatusCode);

            var role = await responseGetRoles.Content.ReadAsAsync<GetRoleForCreateOrUpdateOutput>();
            Assert.False(string.IsNullOrEmpty(role.Role.Name));
        }

        [Fact]
        public async Task Should_Create_Role()
        {
            var input = new CreateOrUpdateRoleInput
            {
                Role = new RoleDto
                {
                    Name = "TestRoleName_" + Guid.NewGuid()
                },
                GrantedPermissionIds = new List<Guid> { DefaultPermissions.MemberAccess.Id }
            };

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/api/roles");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            requestMessage.Content = input.ToStringContent(Encoding.UTF8, "application/json");
            var responseAddRole = await TestServer.CreateClient().SendAsync(requestMessage);
            Assert.Equal(HttpStatusCode.OK, responseAddRole.StatusCode);

            var insertedRole = await _dbContext.Roles.FirstAsync(r => r.Name == input.Role.Name);
            Assert.NotNull(insertedRole);
        }

        [Fact]
        public async Task Should_Update_Role()
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

            var requestMessage = new HttpRequestMessage(HttpMethod.Put, "/api/roles");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            requestMessage.Content = input.ToStringContent(Encoding.UTF8, "application/json");
            var responseAddRole = await TestServer.CreateClient().SendAsync(requestMessage);
            Assert.Equal(HttpStatusCode.OK, responseAddRole.StatusCode);

            var dbContextFromAnotherScope = TestServer.Host.Services.GetRequiredService<NucleusDbContext>();
            var editedTestRole = await dbContextFromAnotherScope.Roles.FindAsync(testRole.Id);
            Assert.Contains(editedTestRole.RolePermissions, rp => rp.PermissionId == DefaultPermissions.MemberAccess.Id);
        }

        [Fact]
        public async Task Should_Delete_Role()
        {
            var testRole = await CreateAndGetTestRoleAsync();
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, "/api/roles?id=" + testRole.Id);

            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            requestMessage.Content = new { id = testRole.Id }.ToStringContent(Encoding.UTF8, "application/json");
            var responseAddRole = await TestServer.CreateClient().SendAsync(requestMessage);
            Assert.Equal(HttpStatusCode.OK, responseAddRole.StatusCode);
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
