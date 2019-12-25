using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Nucleus.Application.Dto.Account;
using Nucleus.Application.Permissions.Dto;
using Nucleus.Application.Users.Dto;
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
        private readonly string _token;

        public AccountControllerTests()
        {
            _token = LoginAsAdminUserAndGetTokenAsync().Result;
        }

        [Fact]
        public async Task Should_Not_Access_Authorized_Controller()
        {
            var responseUsers = await TestServer.CreateClient().GetAsync("/api/users");
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
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/api/users");
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

            var responseRegister = await TestServer.CreateClient().PostAsync("/api/register",
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

            var responseRegister = await TestServer.CreateClient().PostAsync("/api/register",
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

            var response = await TestServer.CreateClient().PostAsync("/api/register",
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

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/api/changePassword");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            requestMessage.Content = input.ToStringContent(Encoding.UTF8, "application/json");
            var response = await TestServer.CreateClient().SendAsync(requestMessage);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Should_Get_Password_Reset_Token()
        {
            var testUser = await CreateAndGetTestUserAsync();
            var token = await LoginAndGetTokenAsync(testUser.UserName, "123qwe");
            var input = new ForgotPasswordInput
            {
                UserNameOrEmail = testUser.Email
            };

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/api/forgotPassword");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            requestMessage.Content = input.ToStringContent(Encoding.UTF8, "application/json");
           
            var response = await TestServer.CreateClient().SendAsync(requestMessage);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var result = await response.Content.ReadAsAsync<ForgotPasswordOutput>();
            Assert.NotNull(result.ResetToken);
        }

        [Fact]
        public async Task Should_Reset_Password()
        {
            var testUser = await CreateAndGetTestUserAsync();
            var token = await LoginAndGetTokenAsync(testUser.UserName, "123qwe");
            var input = new ForgotPasswordInput
            {
                UserNameOrEmail = testUser.Email
            };

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/api/forgotPassword");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            requestMessage.Content = input.ToStringContent(Encoding.UTF8, "application/json");
            var response = await TestServer.CreateClient().SendAsync(requestMessage);
            var result = await response.Content.ReadAsAsync<ForgotPasswordOutput>();

            var inputResetPassword = new ResetPasswordInput
            {
                UserNameOrEmail = testUser.Email,
                Token = result.ResetToken,
                Password = "aA!123456_123123"
            };

            var requestMessageResetPassword = new HttpRequestMessage(HttpMethod.Post, "/api/resetPassword");
            requestMessageResetPassword.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            requestMessageResetPassword.Content = inputResetPassword.ToStringContent(Encoding.UTF8, "application/json");
            
            var responseResetPassword = await TestServer.CreateClient().SendAsync(requestMessageResetPassword);
            Assert.Equal(HttpStatusCode.OK, responseResetPassword.StatusCode);
        }
    }
}
