using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Nucleus.Application.Users;
using Nucleus.Application.Users.Dto;
using Nucleus.Core.Roles;
using Nucleus.Core.Users;
using Nucleus.EntityFramework;
using Xunit;

namespace Nucleus.Tests.Application.Users
{
    public class UserAppServiceTests : ApplicationTestBase
    {
        private readonly NucleusDbContext _dbContext;
        private readonly IUserAppService _userAppService;

        public UserAppServiceTests()
        {
            var serviceProvider = TestServer.Host.Services.CreateScope().ServiceProvider;
            _dbContext = serviceProvider.GetRequiredService<NucleusDbContext>();
            _userAppService = serviceProvider.GetRequiredService<IUserAppService>();
        }

        [Fact]
        public async void Should_Get_Users()
        {
            var userListInput = new UserListInput();
            var userList = await _userAppService.GetUsersAsync(userListInput);
            Assert.True(userList.Items.Count > 0);

            userListInput.Filter = ".!1Aa_";
            var usersListEmpty = await _userAppService.GetUsersAsync(userListInput);
            Assert.True(usersListEmpty.Items.Count == 0);
        }

        [Fact]
        public async void Should_Get_User_For_Create()
        {
            var user = await _userAppService.GetUserForCreateOrUpdateAsync(Guid.Empty);
            Assert.True(string.IsNullOrEmpty(user.User.UserName));
        }

        [Fact]
        public async void Should_Get_User_For_Update()
        {
            var user = await _userAppService.GetUserForCreateOrUpdateAsync(DefaultUsers.Member.Id);
            Assert.False(string.IsNullOrEmpty(user.User.UserName));
        }

        [Fact]
        public async void Should_Add_User()
        {
            var input = new CreateOrUpdateUserInput
            {
                User = new UserDto
                {
                    Id = Guid.NewGuid(),
                    UserName = "TestUserName_" + Guid.NewGuid(),
                    Email = "TestEmail_" + Guid.NewGuid() + "@mail.com",
                    Password = "aA!121212"
                },
                GrantedRoleIds = new List<Guid> { DefaultRoles.Member.Id }
            };

            await _userAppService.AddUserAsync(input);
            await _dbContext.SaveChangesAsync();

            var dbContextFromAnotherScope = TestServer.Host.Services.GetRequiredService<NucleusDbContext>();
            var insertedTestUser = await dbContextFromAnotherScope.Users.FindAsync(input.User.Id);

            Assert.NotNull(insertedTestUser);
            Assert.Equal(1, insertedTestUser.UserRoles.Count);
        }

        [Fact]
        public async void Should_Edit_User()
        {
            var testUser = await CreateAndGetTestUserAsync();

            var input = new CreateOrUpdateUserInput
            {
                User = new UserDto
                {
                    Id = testUser.Id,
                    UserName = "TestUserName_Edited_" + Guid.NewGuid(),
                    Email = "TestEmail_" + Guid.NewGuid() + "@mail.com"
                },
                GrantedRoleIds = new List<Guid> { DefaultRoles.Member.Id }
            };
            await _userAppService.EditUserAsync(input);
            var editedTestUser = await _dbContext.Users.FindAsync(testUser.Id);

            Assert.Contains("TestUserName_Edited_", editedTestUser.UserName);
            Assert.Contains(editedTestUser.UserRoles, ur => ur.RoleId == DefaultRoles.Member.Id);
        }

        [Fact]
        public async void Should_Remove_User()
        {
            var testUser = await CreateAndGetTestUserAsync();

            var dbContextFromAnotherScope = TestServer.Host.Services.GetRequiredService<NucleusDbContext>();
            var insertedTestUser = await dbContextFromAnotherScope.Users.FindAsync(testUser.Id);

            Assert.NotNull(insertedTestUser);
            Assert.Equal(1, insertedTestUser.UserRoles.Count);

            await _userAppService.RemoveUserAsync(insertedTestUser.Id);
            _dbContext.SaveChanges();

            dbContextFromAnotherScope = TestServer.Host.Services.GetRequiredService<NucleusDbContext>();
            var removedTestUser = await dbContextFromAnotherScope.Users.FindAsync(testUser.Id);
            var removedRoleMatches = dbContextFromAnotherScope.UserRoles.Where(rp => rp.UserId == testUser.Id);

            Assert.Null(removedTestUser);
            Assert.Equal(0, removedRoleMatches.Count());
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
