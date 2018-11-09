using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
    public class UserControllerTests : ApiTestBase
    {
        private readonly NucleusDbContext _dbContext;
        private readonly string _token;

        public UserControllerTests()
        {
            _dbContext = TestServer.Host.Services.GetRequiredService<NucleusDbContext>();
            _token = LoginAsAdminUserAndGetTokenAsync().Result;
        }

        [Fact]
        public async Task Should_Get_Users()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/api/user/getUsers");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var responseGetUsers = await TestServer.CreateClient().SendAsync(requestMessage);
            Assert.Equal(HttpStatusCode.OK, responseGetUsers.StatusCode);

            var users = await responseGetUsers.Content.ReadAsAsync<PagedList<UserListOutput>>();
            Assert.True(users.Items.Any());
        }

        [Fact]
        public async Task Should_Get_User_For_Create()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/api/user/getUserForCreateOrUpdate?id=" + Guid.Empty);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var responseGetUsers = await TestServer.CreateClient().SendAsync(requestMessage);
            Assert.Equal(HttpStatusCode.OK, responseGetUsers.StatusCode);

            var user = await responseGetUsers.Content.ReadAsAsync<GetUserForCreateOrUpdateOutput>();
            Assert.True(string.IsNullOrEmpty(user.User.UserName));
        }

        [Fact]
        public async Task Should_Get_User_For_Update()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/api/user/getUserForCreateOrUpdate?id=" + DefaultUsers.Member.Id);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var responseGetUsers = await TestServer.CreateClient().SendAsync(requestMessage);
            Assert.Equal(HttpStatusCode.OK, responseGetUsers.StatusCode);

            var user = await responseGetUsers.Content.ReadAsAsync<GetUserForCreateOrUpdateOutput>();
            Assert.False(string.IsNullOrEmpty(user.User.UserName));
        }

        [Fact]
        public async Task Should_Create_User()
        {
            var input = new CreateOrUpdateUserInput
            {
                User = new UserDto
                {
                    UserName  = "TestUserName_" + Guid.NewGuid(),
                    Email  = "TestUserEmail_" + Guid.NewGuid(),
                    Password = "aA!121212"
                },
                GrantedRoleIds = new List<Guid> { DefaultRoles.Member.Id }
            };
            
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/api/user/createOrUpdateUser");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            requestMessage.Content = input.ToStringContent(Encoding.UTF8, "application/json");
            var responseAddUser = await TestServer.CreateClient().SendAsync(requestMessage);
            Assert.Equal(HttpStatusCode.OK, responseAddUser.StatusCode);

            var insertedUser = await _dbContext.Users.FirstAsync(u => u.UserName == input.User.UserName);
            Assert.NotNull(insertedUser);
        }

        [Fact]
        public async Task Should_Update_User()
        {
            var testUser = await CreateAndGetTestUserAsync();

            var input = new CreateOrUpdateUserInput
            {
                User = new UserDto
                {
                    Id = testUser.Id,
                    UserName = "TestUserName_Edited_" + Guid.NewGuid(),
                    Email = testUser.Email
                },
                GrantedRoleIds = new List<Guid> { DefaultRoles.Member.Id }
            };
            
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/api/user/createOrUpdateUser");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            requestMessage.Content = input.ToStringContent(Encoding.UTF8, "application/json");
            var responseAddUser = await TestServer.CreateClient().SendAsync(requestMessage);
            Assert.Equal(HttpStatusCode.OK, responseAddUser.StatusCode);

            var dbContextFromAnotherScope = TestServer.Host.Services.GetRequiredService<NucleusDbContext>(); 
            var editedTestUser = await dbContextFromAnotherScope.Users.FindAsync(testUser.Id);
            Assert.Contains(editedTestUser.UserRoles, ur => ur.RoleId == DefaultRoles.Member.Id);
        }

        [Fact]
        public async Task Should_Delete_User()
        {
            var testUser = await CreateAndGetTestUserAsync();
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, "/api/user/deleteUser?id=" + testUser.Id);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var responseAddUser = await TestServer.CreateClient().SendAsync(requestMessage);
            Assert.Equal(HttpStatusCode.OK, responseAddUser.StatusCode);
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
