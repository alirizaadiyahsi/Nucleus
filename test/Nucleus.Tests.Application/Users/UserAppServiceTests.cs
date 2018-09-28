using Microsoft.Extensions.DependencyInjection;
using Nucleus.Application.Users;
using Nucleus.Application.Users.Dto;
using Xunit;

namespace Nucleus.Tests.Application.Users
{
    public class UserAppServiceTests : ApplicationTestBase
    {
        private readonly IUserAppService _userAppService;

        public UserAppServiceTests()
        {
            _userAppService = TestServer.Host.Services.GetRequiredService<IUserAppService>();
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
    }
}
