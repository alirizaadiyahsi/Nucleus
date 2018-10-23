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
using Nucleus.Web.Api.Models;
using Xunit;

namespace Nucleus.Tests.Web.Api.Controllers
{
    public class RoleControllerTests : ApiTestBase
    {
        private readonly NucleusDbContext _dbContext;

        public RoleControllerTests()
        {
            _dbContext = TestServer.Host.Services.GetRequiredService<NucleusDbContext>();
        }

        [Fact]
        public async Task Should_Get_Roles()
        {
            var responseLogin = await LoginAsAdminUserAsync();
            var responseContent = await responseLogin.Content.ReadAsAsync<LoginResult>();
            var token = responseContent.Token;

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/api/role/getroles");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var responseGetUsers = await TestServer.CreateClient().SendAsync(requestMessage);
            Assert.Equal(HttpStatusCode.OK, responseGetUsers.StatusCode);

            var roles = await responseGetUsers.Content.ReadAsAsync<PagedList<RoleListOutput>>();
            Assert.True(roles.Items.Any());
        }

        [Fact]
        public async Task Should_Get_Role_For_Create()
        {
            var responseLogin = await LoginAsAdminUserAsync();
            var responseContent = await responseLogin.Content.ReadAsAsync<LoginResult>();
            var token = responseContent.Token;

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/api/role/getRoleForCreateOrUpdate?id=" + Guid.Empty);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var responseGetUsers = await TestServer.CreateClient().SendAsync(requestMessage);
            Assert.Equal(HttpStatusCode.OK, responseGetUsers.StatusCode);

            var role = await responseGetUsers.Content.ReadAsAsync<GetRoleForCreateOrUpdateOutput>();
            Assert.True(string.IsNullOrEmpty(role.Role.Name));
        }

        [Fact]
        public async Task Should_Get_Role_For_Update()
        {
            var responseLogin = await LoginAsAdminUserAsync();
            var responseContent = await responseLogin.Content.ReadAsAsync<LoginResult>();
            var token = responseContent.Token;

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/api/role/getRoleForCreateOrUpdate?id=" + DefaultRoles.Member.Id);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var responseGetUsers = await TestServer.CreateClient().SendAsync(requestMessage);
            Assert.Equal(HttpStatusCode.OK, responseGetUsers.StatusCode);

            var role = await responseGetUsers.Content.ReadAsAsync<GetRoleForCreateOrUpdateOutput>();
            Assert.False(string.IsNullOrEmpty(role.Role.Name));
        }

        [Fact]
        public async Task Should_Create_Role()
        {
            var input = new CreateOrUpdateRoleInput
            {
                Role = new RoleDto
                {
                    Name = "TestRole_" + Guid.NewGuid()
                },
                GrantedPermissionIds = new List<Guid> { DefaultPermissions.MemberAccess.Id }
            };

            var token = await LoginAsAdminUserAndGetTokenAsync();
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/api/role/createOrUpdateRole");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            requestMessage.Content = input.ToStringContent(Encoding.UTF8, "application/json");
            var responseAddRole = await TestServer.CreateClient().SendAsync(requestMessage);
            Assert.Equal(HttpStatusCode.OK, responseAddRole.StatusCode);

            var insertedRole = await _dbContext.Roles.FirstAsync(r => r.Name == input.Role.Name);
            Assert.NotNull(insertedRole);
        }

        [Fact]
        public async Task Should_Update_Role()
        {
            var testRole = await CreateAndGetTestRole();

            var input = new CreateOrUpdateRoleInput
            {
                Role = new RoleDto
                {
                    Id = testRole.Id,
                    Name = "TestRoleName_Edited_" + Guid.NewGuid()
                },
                GrantedPermissionIds = new List<Guid> { DefaultPermissions.MemberAccess.Id }
            };

            var token = await LoginAsAdminUserAndGetTokenAsync();
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/api/role/createOrUpdateRole");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
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
            var testRole = await CreateAndGetTestRole();

            var token = await LoginAsAdminUserAndGetTokenAsync();
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, "/api/role/deleteRole");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            requestMessage.Content = new { id = testRole.Id }.ToStringContent(Encoding.UTF8, "application/json");
            var responseAddRole = await TestServer.CreateClient().SendAsync(requestMessage);

            Assert.Equal(HttpStatusCode.OK, responseAddRole.StatusCode);
        }

        private async Task<Role> CreateAndGetTestRole()
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
