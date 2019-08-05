using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Nucleus.Application.Dto.Account;
using Nucleus.Core.Users;
using Nucleus.Tests.Shared;
using Nucleus.Utilities.Extensions.PrimitiveTypes;

namespace Nucleus.Tests.Web.Api
{
    public class ApiTestBase : TestBase
    {
        private static readonly LoginInput AdminUserLoginViewModel = new LoginInput
        {
            UserNameOrEmail = DefaultUsers.TestAdmin.Email,
            Password = "123qwe"
        };

        protected async Task<HttpResponseMessage> LoginAsAdminUserAsync()
        {
            return await TestServer.CreateClient().PostAsync("/api/login",
                AdminUserLoginViewModel.ToStringContent(Encoding.UTF8, "application/json"));
        }

        protected async Task<HttpResponseMessage> LoginAsync(string userNameOrEmail, string password)
        {
            var adminUserLoginViewModel = new LoginInput
            {
                UserNameOrEmail = userNameOrEmail,
                Password = password
            };

            return await TestServer.CreateClient().PostAsync("/api/login",
                adminUserLoginViewModel.ToStringContent(Encoding.UTF8, "application/json"));
        }

        protected async Task<string> LoginAsAdminUserAndGetTokenAsync()
        {
            var responseLogin = await LoginAsAdminUserAsync();
            if (!responseLogin.IsSuccessStatusCode)
            {
                return null;
            }

            var loginResult = await responseLogin.Content.ReadAsAsync<LoginOutput>();
            return loginResult.Token;
        }

        protected async Task<string> LoginAndGetTokenAsync(string userNameOrEmail, string password)
        {
            var responseLogin = await LoginAsync(userNameOrEmail, password);
            if (!responseLogin.IsSuccessStatusCode)
            {
                return null;
            }

            var loginResult = await responseLogin.Content.ReadAsAsync<LoginOutput>();
            return loginResult.Token;
        }
    }
}