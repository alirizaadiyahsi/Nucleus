using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Nucleus.Application.Permissions;
using Nucleus.Application.Permissions.Dto;
using Nucleus.Core.Permissions;
using Nucleus.Core.Roles;
using Xunit;

namespace Nucleus.Tests.Application.Permissions
{
    public class PermissionAppServiceTests : ApplicationTestBase
    {
        private readonly IPermissionAppService _permissionAppService;

        public PermissionAppServiceTests()
        {
            _permissionAppService = TestServer.Host.Services.GetRequiredService<IPermissionAppService>();
        }

        [Fact]
        public async void TestGetPermissions()
        {
            var permissionListInput = new PermissionListInput();
            var permissionList = await _permissionAppService.GetPermissionsAsync(permissionListInput);
            Assert.True(permissionList.Items.Count >= 0);

            permissionListInput.Filter = ".!1Aa_";
            var permissionListEmpty = await _permissionAppService.GetPermissionsAsync(permissionListInput);
            Assert.True(permissionListEmpty.Items.Count == 0);
        }

        [Fact]
        public async Task TestIsPermissionGrantedForUser()
        {
            var isPermissionGranted =
                await _permissionAppService.IsPermissionGrantedToUserAsync(ContextUser.Identity.Name, DefaultPermissions.MemberAccess.Id);

            var isPermissionNotGranted =
                await _permissionAppService.IsPermissionGrantedToUserAsync(null, DefaultPermissions.MemberAccess.Id);

            Assert.True(isPermissionGranted);
            Assert.False(isPermissionNotGranted);
        }

        [Fact]
        public async Task TestIsPermissionGrantedForRole()
        {
            var isPermissionGranted =
                await _permissionAppService.IsPermissionGrantedToRoleAsync(DefaultRoles.Member, DefaultPermissions.MemberAccess);

            var isPermissionNotGranted =
                await _permissionAppService.IsPermissionGrantedToRoleAsync(new Role(), DefaultPermissions.AdministrationAccess);

            Assert.True(isPermissionGranted);
            Assert.False(isPermissionNotGranted);
        }
    }
}