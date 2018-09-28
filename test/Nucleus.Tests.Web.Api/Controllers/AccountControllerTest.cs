using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Nucleus.Application.Users.Dto;
using Nucleus.Utilities.Collections;
using Nucleus.Utilities.Extensions.Collections;
using Nucleus.Web.Api.App.Account.Models;
using Xunit;

namespace Nucleus.Tests.Web.Api.Controllers
{
    public class AccountTests : ApiTestBase
    {
        [Fact]
        public async Task Should_Not_Access_Authorized_Controller()
        {
            var responseUsers = await TestServer.CreateClient().GetAsync("/api/user");
            Assert.Equal(HttpStatusCode.Unauthorized, responseUsers.StatusCode);
        }

        [Fact]
        public async Task Should_Login()
        {
            var responseLogin = await LoginAsAdminUserAsync();
            Assert.Equal(HttpStatusCode.OK, responseLogin.StatusCode);
        }

        [Fact]
        public async Task Shoudl_Get_Token()
        {
            var responseLogin = await LoginAsAdminUserAsync();
            var loginResult = await responseLogin.Content.ReadAsAsync<LoginResult>();
            Assert.NotNull(loginResult.Token);
        }

        [Fact]
        public async Task Should_Access_Authorized_Controller()
        {
            var responseLogin = await LoginAsAdminUserAsync();
            var responseContent = await responseLogin.Content.ReadAsAsync<LoginResult>();
            var token = responseContent.Token;

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/api/user");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var responseGetUsers = await TestServer.CreateClient().SendAsync(requestMessage);
            Assert.Equal(HttpStatusCode.OK, responseGetUsers.StatusCode);

            var users = await responseGetUsers.Content.ReadAsAsync<PagedList<UserListOutput>>();
            Assert.True(users.Items.Any());
        }

        [Fact]
        public async Task Should_Register()
        {
            var registrationData = new Dictionary<string, string>
            {
                {"username",  "TestUserName_" + Guid.NewGuid()},
                {"email",  "TestUserEmail_" + Guid.NewGuid() + "@mail.com"},
                {"password", "aA!121212"}
            };

            var responseRegister = await TestServer.CreateClient().PostAsync("/api/account/register",
                registrationData.ToStringContent(Encoding.UTF8, "application/json"));

            Assert.Equal(HttpStatusCode.OK, responseRegister.StatusCode);
        }
    }
}
