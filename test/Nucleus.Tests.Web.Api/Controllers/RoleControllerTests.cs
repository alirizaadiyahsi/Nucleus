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
        public async Task Should_Delete_Role()
        {
            var roleToInsert = new Role { Id = Guid.NewGuid(), Name = "TestRoleName_" + Guid.NewGuid() };
            await _dbContext.Roles.AddAsync(roleToInsert);
            await _dbContext.RolePermissions.AddAsync(new RolePermission
            {
                RoleId = roleToInsert.Id,
                PermissionId = DefaultPermissions.RoleRead.Id
            });
            await _dbContext.SaveChangesAsync();

            var token = await LoginAsAdminUserAndGetTokenAsync();
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, "/api/role/deleteRole");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            requestMessage.Content = new { id = roleToInsert.Id }.ToStringContent(Encoding.UTF8, "application/json");
            var responseAddRole = await TestServer.CreateClient().SendAsync(requestMessage);

            Assert.Equal(HttpStatusCode.OK, responseAddRole.StatusCode);
        }
    }
}
