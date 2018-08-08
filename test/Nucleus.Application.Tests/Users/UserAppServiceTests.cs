using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Nucleus.Application.Users;
using Nucleus.Application.Users.Dto;
using Nucleus.EntityFramework;
using Xunit;

namespace Nucleus.Application.Tests.Users
{
    public class UserAppServiceTests : ApplicationTestBase
    {
        private readonly IUserAppService _userAppService;

        public UserAppServiceTests()
        {
            var dbContext = TestServer.Host.Services.GetRequiredService<NucleusDbContext>();
            var mapper = TestServer.Host.Services.GetRequiredService<IMapper>();
            _userAppService = new UserAppService(dbContext, mapper);
        }

        [Fact]
        public async void TestGetUsers()
        {
            var userListInput = new UserListInput();
            var userList = await _userAppService.GetUsersAsync(userListInput);
            Assert.True(userList.Items.Count >= 0);

            userListInput.Filter = "qwerty";
            var usersListEmpty = await _userAppService.GetUsersAsync(userListInput);
            Assert.True(usersListEmpty.Items.Count == 0);
        }
    }
}
