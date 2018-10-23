using System;
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
            Assert.True(userList.Items.Count >= 0);

            userListInput.Filter = ".!1Aa_";
            var usersListEmpty = await _userAppService.GetUsersAsync(userListInput);
            Assert.True(usersListEmpty.Items.Count == 0);
        }

        [Fact]
        public async void Should_Remove_User()
        {
            var testUser = await CreateAndGetTestUser();

            var dbContextFromAnotherScope = TestServer.Host.Services.GetRequiredService<NucleusDbContext>();
            var insertedTestUser = await dbContextFromAnotherScope.Users.FindAsync(testUser.Id);

            Assert.NotNull(insertedTestUser);
            Assert.Equal(1, insertedTestUser.UserRoles.Count);

            _userAppService.RemoveUser(insertedTestUser.Id);
            _dbContext.SaveChanges();

            dbContextFromAnotherScope = TestServer.Host.Services.GetRequiredService<NucleusDbContext>();
            var removedTestUser = await dbContextFromAnotherScope.Users.FindAsync(testUser.Id);
            var removedRoleMatches = dbContextFromAnotherScope.UserRoles.Where(rp => rp.UserId == testUser.Id);

            Assert.Null(removedTestUser);
            Assert.Equal(0, removedRoleMatches.Count());
        }

        private async Task<User> CreateAndGetTestUser()
        {
            var testUser = new User
            {
                Id = Guid.NewGuid(), 
                UserName = "TestUserName_" + Guid.NewGuid(),
                Email =  "TestUserEmail_" + Guid.NewGuid(),
                PasswordHash = "AM4OLBpptxBYmM79lGOX9egzZk3vIQU3d/gFCJzaBjAPXzYIK3tQ2N7X4fcrHtElTw==" //123qwe
            };
            await _dbContext.Users.AddAsync(testUser);
            await _dbContext.UserRoles.AddAsync(new UserRole
            {
                UserId = testUser.Id,
                RoleId = DefaultRoles.Member.Id
            });
            await _dbContext.SaveChangesAsync();
            return testUser;
        }
    }
}
