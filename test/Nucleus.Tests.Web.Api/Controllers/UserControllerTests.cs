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
    public class UserControllerTests: ApiTestBase
    {
        [Fact]
        public async Task Should_Get_Users()
        {
            var responseLogin = await LoginAsAdminUserAsync();
            var responseContent = await responseLogin.Content.ReadAsAsync<LoginResult>();
            var token = responseContent.Token;

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/api/user/getUsers");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var responseGetUsers = await TestServer.CreateClient().SendAsync(requestMessage);
            Assert.Equal(HttpStatusCode.OK, responseGetUsers.StatusCode);

            var users = await responseGetUsers.Content.ReadAsAsync<PagedList<UserListOutput>>();
            Assert.True(users.Items.Any());
        }
    }
}
