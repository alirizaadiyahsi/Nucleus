using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Nucleus.Application.Permissions.Dto;
using Nucleus.Application.Roles.Dto;
using Nucleus.Core.Permissions;
using Nucleus.Utilities.Collections;
using Nucleus.Utilities.Extensions.PrimitiveTypes;
using Nucleus.Web.Api.Models;
using Xunit;

namespace Nucleus.Tests.Web.Api.Controllers
{
    public class RoleControllerTests : ApiTestBase
    {
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
        public async Task Should_CreateRole()
        {
            var addRoleInput = new CreateOrEditRoleInput
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

            var token = await LoginAsAdminUserAndGetTokenAsync();
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/api/role/addRole");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            requestMessage.Content = addRoleInput.ToStringContent(Encoding.UTF8, "application/json");
            var responseAddRole = await TestServer.CreateClient().SendAsync(requestMessage);

            Assert.Equal(HttpStatusCode.OK, responseAddRole.StatusCode);
        }
    }
}
