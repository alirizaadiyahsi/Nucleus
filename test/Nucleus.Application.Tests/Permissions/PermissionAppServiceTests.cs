using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Nucleus.Application.Permissions;
using Nucleus.Application.Permissions.Dto;
using Nucleus.Core.Permissions;
using Nucleus.EntityFramework;
using Xunit;

namespace Nucleus.Application.Tests.Permissions
{
    public class PermissionAppServiceTests : ApplicationTestBase
    {
        private readonly IPermissionAppService _permissionAppService;

        public PermissionAppServiceTests()
        {
            var dbContext = TestServer.Host.Services.GetRequiredService<NucleusDbContext>();
            var mapper = TestServer.Host.Services.GetRequiredService<IMapper>();
            _permissionAppService = new PermissionAppService(dbContext, mapper);
        }

        [Fact]
        public async void TestGetPermissions()
        {
            var permissionListInput = new PermissionListInput();
            var permissionsList = await _permissionAppService.GetPermissionsAsync(permissionListInput);
            Assert.True(permissionsList.Items.Count >= 0);

            permissionListInput.Filter = "qwerty";
            var permissionsListEmpty = await _permissionAppService.GetPermissionsAsync(permissionListInput);
            Assert.True(permissionsListEmpty.Items.Count == 0);
        }

        [Fact]
        public async Task TestIsPermissionGrantedForUser()
        {
            var isPermissionGranted =
                await _permissionAppService.IsPermissionGrantedToUserAsync(ContextUser, DefaultPermissions.MemberAccess);

            Assert.True(isPermissionGranted);
        }
    }
}
