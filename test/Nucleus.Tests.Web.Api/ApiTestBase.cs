using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Nucleus.Core.Users;
using Nucleus.Tests.Shared;
using Nucleus.Utilities.Extensions.PrimitiveTypes;
using Nucleus.Web.Api.Models;

namespace Nucleus.Tests.Web.Api
{
    public class ApiTestBase : TestBase
    {
        private static readonly LoginViewModel AdminUserLoginViewModel = new LoginViewModel
        {
            UserNameOrEmail = DefaultUsers.Admin.Email,
            Password = "123qwe"
        };

        protected async Task<HttpResponseMessage> LoginAsAdminUserAsync()
        {
            return await TestServer.CreateClient().PostAsync("/api/account/login",
                AdminUserLoginViewModel.ToStringContent(Encoding.UTF8, "application/json"));
        }

        protected async Task<HttpResponseMessage> LoginAsync(string userNameOrEmail, string password)
        {
            var adminUserLoginViewModel = new LoginViewModel
            {
                UserNameOrEmail = userNameOrEmail,
                Password = password
            };

            return await TestServer.CreateClient().PostAsync("/api/account/login",
                adminUserLoginViewModel.ToStringContent(Encoding.UTF8, "application/json"));
        }

        protected async Task<string> LoginAsAdminUserAndGetTokenAsync()
        {
            var responseLogin = await LoginAsAdminUserAsync();
            var loginResult = await responseLogin.Content.ReadAsAsync<LoginResult>();

            return loginResult.Token;
        }
    }
}