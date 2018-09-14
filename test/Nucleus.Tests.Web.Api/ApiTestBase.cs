using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Nucleus.Core.Users;
using Nucleus.Tests.Shared;
using Nucleus.Utilities.Extensions.Collections;

namespace Nucleus.Tests.Web.Api
{
    public class ApiTestBase : TestBase
    {
        private static readonly Dictionary<string, string> ApiUserFormData = new Dictionary<string, string>
        {
            {"email",  DefaultUsers.Member.Email},
            {"username",  DefaultUsers.Member.UserName},
            {"password", "123qwe"}
        };

        protected async Task<HttpResponseMessage> LoginAsApiUserAsync()
        {
            return await TestServer.CreateClient().PostAsync("/api/account/login",
                ApiUserFormData.ToStringContent(Encoding.UTF8, "application/json"));
        }
    }
}