using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Nucleus.Application.Users.Dto;
using Nucleus.Core.Permissions;
using Nucleus.Core.Roles;
using Nucleus.Core.Users;
using Nucleus.EntityFramework;
using Nucleus.Utilities.Collections;
using Nucleus.Utilities.Extensions.PrimitiveTypes;
using Xunit;

namespace Nucleus.Tests.Web.Api.Controllers
{
    public class AccountControllerTests : ApiTestBase
    {
        private readonly NucleusDbContext _dbContext;
        private readonly string _token;

        public AccountControllerTests()
        {
            _dbContext = TestServer.Host.Services.GetRequiredService<NucleusDbContext>();
            _token = LoginAsAdminUserAndGetTokenAsync().Result;
        }

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
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/api/user/getUsers");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);
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
            var input = new RegisterInput
            {
                Email = new string('*', 300),
                UserName = new string('*', 300),
                Password = "aA!121212"
            };

            var response = await TestServer.CreateClient().PostAsync("/api/account/register",
                input.ToStringContent(Encoding.UTF8, "application/json"));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Should_Change_Password()
        {
            var testUser = await CreateAndGetTestUserAsync();
            var token = await LoginAndGetTokenAsync(testUser.UserName, "123qwe");
            var input = new ChangePasswordInput
            {
                CurrentPassword = "123qwe",
                NewPassword = "aA!121212",
                PasswordRepeat = "aA!121212"
            };

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/api/account/changePassword");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            requestMessage.Content = input.ToStringContent(Encoding.UTF8, "application/json");
            var response = await TestServer.CreateClient().SendAsync(requestMessage);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Should_User_Is_In_Role()
        {
            var testUser = await CreateAndGetTestUserAsync();
            var token = await LoginAndGetTokenAsync(testUser.UserName, "123qwe");
            var input = new IsUserInRoleInput
            {
                UserNameOrEmail = DefaultUsers.TestAdmin.UserName,
                RoleName = DefaultRoles.RoleNameForAdmin
            };

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/api/account/isUserInRole?" + input.ToQueryString());
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await TestServer.CreateClient().SendAsync(requestMessage);
            var isUserInRole = await response.Content.ReadAsAsync<bool>();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(isUserInRole);
        }

        [Fact]
        public async Task Should_User_Grant_To_Permission()
        {
            var testUser = await CreateAndGetTestUserAsync();
            var token = await LoginAndGetTokenAsync(testUser.UserName, "123qwe");
            var input = new IsUserGrantToPermissionInput
            {
                UserNameOrEmail = DefaultUsers.TestAdmin.UserName,
                PermissionName = DefaultPermissions.PermissionNameForAdministration
            };

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/api/account/IsUserGrantToPermissionAsync?" + input.ToQueryString());
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await TestServer.CreateClient().SendAsync(requestMessage);
            var isUserGrantToPermissionAsync = await response.Content.ReadAsAsync<bool>();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(isUserGrantToPermissionAsync);
        }

        private async Task<User> CreateAndGetTestUserAsync()
        {
            var testUser = new User
            {
                Id = Guid.NewGuid(),
                UserName = "TestUserName_" + Guid.NewGuid(),
                Email = "TestUserEmail_" + Guid.NewGuid(),
                PasswordHash = "AM4OLBpptxBYmM79lGOX9egzZk3vIQU3d/gFCJzaBjAPXzYIK3tQ2N7X4fcrHtElTw==" //123qwe
            };
            testUser.NormalizedEmail = testUser.Email.Normalize();
            testUser.NormalizedUserName = testUser.UserName.Normalize();

            await _dbContext.Users.AddAsync(testUser);
            await _dbContext.UserRoles.AddAsync(new UserRole
            {
                UserId = testUser.Id,
                RoleId = DefaultRoles.Admin.Id
            });
            await _dbContext.SaveChangesAsync();
            return testUser;
        }
    }
}
