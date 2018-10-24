using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Nucleus.Application.Users.Dto;
using Nucleus.Core.Roles;
using Nucleus.Core.Users;
using Nucleus.EntityFramework;
using Nucleus.Utilities.Collections;
using Nucleus.Utilities.Extensions.PrimitiveTypes;
using Xunit;

namespace Nucleus.Tests.Web.Api.Controllers
{
    public class UserControllerTests: ApiTestBase
    {
        private readonly NucleusDbContext _dbContext;

        public UserControllerTests()
        {
            _dbContext = TestServer.Host.Services.GetRequiredService<NucleusDbContext>();
        }

        [Fact]
        public async Task Should_Get_Users()
        {
            var responseLogin = await LoginAsAdminUserAsync();
            var responseContent = await responseLogin.Content.ReadAsAsync<LoginOutput>();
            var token = responseContent.Token;

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/api/user/getUsers");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var responseGetUsers = await TestServer.CreateClient().SendAsync(requestMessage);
            Assert.Equal(HttpStatusCode.OK, responseGetUsers.StatusCode);

            var users = await responseGetUsers.Content.ReadAsAsync<PagedList<UserListOutput>>();
            Assert.True(users.Items.Any());
        }

        [Fact]
        public async Task Should_Delete_User()
        {
            var testUser = await CreateAndGetTestUser();

            var token = await LoginAsAdminUserAndGetTokenAsync();
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, "/api/user/deleteUser");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            requestMessage.Content = new { id = testUser.Id }.ToStringContent(Encoding.UTF8, "application/json");
            var responseAddUser = await TestServer.CreateClient().SendAsync(requestMessage);

            Assert.Equal(HttpStatusCode.OK, responseAddUser.StatusCode);
        }

        private async Task<User> CreateAndGetTestUser()
        {
            var testUser = new User
            {
                Id = Guid.NewGuid(), 
                UserName = "TestUserName_" + Guid.NewGuid(),
                Email =  "TestUserEmail_" + Guid.NewGuid(),
                PasswordHash = "AM4OLBpptxBYmM79lGOX9egzZk3vIQU3d/gFCJzaBjAPXzYIK3tQ2N7X4fcrHtElTw==" //123qwe
            };
            await _dbContext.Users.AddAsync(testUser);
            await _dbContext.UserRoles.AddAsync(new UserRole
            {
                UserId = testUser.Id,
                RoleId = DefaultRoles.Member.Id
            });
            await _dbContext.SaveChangesAsync();
            return testUser;
        }
    }
}
