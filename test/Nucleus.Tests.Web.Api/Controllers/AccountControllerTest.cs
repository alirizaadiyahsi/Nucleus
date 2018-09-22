using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Nucleus.Application.Users.Dto;
using Nucleus.Utilities.Collections;
using Nucleus.Web.Api.Models;
using Xunit;

namespace Nucleus.Tests.Web.Api.Controllers
{
    public class AccountTests : ApiTestBase
    {
        [Fact]
        public async Task TestUnAuthorizedAccess()
        {
            var responseGetUsers = await TestServer.CreateClient().GetAsync("/api/user/Users");
            Assert.Equal(HttpStatusCode.Unauthorized, responseGetUsers.StatusCode);
        }

        [Fact]
        public async Task TestLogin()
        {
            var responseLogin = await LoginAsApiUserAsync();
            Assert.Equal(HttpStatusCode.OK, responseLogin.StatusCode);
        }

        [Fact]
        public async Task TestGetToken()
        {
            var responseLogin = await LoginAsApiUserAsync();
            var loginResult = await responseLogin.Content.ReadAsAsync<LoginResult>();
            Assert.NotNull(loginResult.Token);
        }

        [Fact]
        public async Task TestAuthorizedAccess()
        {
            var responseLogin = await LoginAsApiUserAsync();
            var responseContent = await responseLogin.Content.ReadAsAsync<LoginResult>();
            var token = responseContent.Token;

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/api/user/users/");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var responseGetUsers = await TestServer.CreateClient().SendAsync(requestMessage);
            Assert.Equal(HttpStatusCode.OK, responseGetUsers.StatusCode);

            var users = await responseGetUsers.Content.ReadAsAsync<PagedList<UserListOutput>>();
            Assert.True(users.Items.Any());
        }
    }
}
