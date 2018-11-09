using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Nucleus.Application.Users.Dto;
using Nucleus.Core.Users;
using Nucleus.Utilities.Collections;
using Nucleus.Utilities.Extensions.PrimitiveTypes;
using Xunit;

namespace Nucleus.Tests.Web.Api.Controllers
{
    public class AccountControllerTests : ApiTestBase
    {
        [Fact]
        public async Task Should_Not_Access_Authorized_Controller()
        {
            var responseUsers = await TestServer.CreateClient().GetAsync("/api/user/getUsers");
            Assert.Equal(HttpStatusCode.Unauthorized, responseUsers.StatusCode);
        }

        [Fact]
        public async Task Should_Login()
        {
            var responseLogin = await LoginAsAdminUserAsync();
            Assert.Equal(HttpStatusCode.OK, responseLogin.StatusCode);
        }

        [Fact]
        public async Task Should_Not_Login_With_Wrong_Credentials()
        {
            var responseLogin = await LoginAsync("wrongUserName", "wrongPassword");
            Assert.Equal(HttpStatusCode.BadRequest, responseLogin.StatusCode);
        }

        [Fact]
        public async Task Should_Get_Token()
        {
            var responseLogin = await LoginAsAdminUserAsync();
            var loginResult = await responseLogin.Content.ReadAsAsync<LoginOutput>();
            Assert.NotNull(loginResult.Token);
        }

        [Fact]
        public async Task Should_Access_Authorized_Controller()
        {
            var token = await LoginAsAdminUserAndGetTokenAsync();
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/api/user/getUsers");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var responseGetUsers = await TestServer.CreateClient().SendAsync(requestMessage);
            Assert.Equal(HttpStatusCode.OK, responseGetUsers.StatusCode);

            var users = await responseGetUsers.Content.ReadAsAsync<PagedList<UserListOutput>>();
            Assert.True(users.Items.Any());
        }

        [Fact]
        public async Task Should_Register()
        {
            var registerInput = new RegisterInput
            {
                Email = "TestUserEmail_" + Guid.NewGuid() + "@mail.com",
                UserName = "TestUserName_" + Guid.NewGuid(),
                Password = "aA!121212"
            };

            var responseRegister = await TestServer.CreateClient().PostAsync("/api/account/register",
                registerInput.ToStringContent(Encoding.UTF8, "application/json"));

            Assert.Equal(HttpStatusCode.OK, responseRegister.StatusCode);
        }

        [Fact]
        public async Task Should_Not_Register_With_Existing_User()
        {
            var registerInput = new RegisterInput
            {
                Email = DefaultUsers.TestAdmin.Email,
                UserName = DefaultUsers.TestAdmin.UserName,
                Password = "aA!121212"
            };

            var responseRegister = await TestServer.CreateClient().PostAsync("/api/account/register",
                registerInput.ToStringContent(Encoding.UTF8, "application/json"));

            Assert.Equal(HttpStatusCode.BadRequest, responseRegister.StatusCode);
        }

        [Fact]
        public async Task Should_Not_Register_With_Invalid_User()
        {
            var registerInput = new RegisterInput
            {
                Email = new string('*', 300),
                UserName = new string('*', 300),
                Password = "aA!121212"
            };

            var responseRegister = await TestServer.CreateClient().PostAsync("/api/account/register",
                registerInput.ToStringContent(Encoding.UTF8, "application/json"));

            Assert.Equal(HttpStatusCode.BadRequest, responseRegister.StatusCode);
        }
    }
}
