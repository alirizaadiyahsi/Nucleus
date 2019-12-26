using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nucleus.Core.Permissions;
using Nucleus.Core.Roles;
using Nucleus.Core.Users;
using Nucleus.EntityFramework;
using Nucleus.Web.Api;

namespace Nucleus.Tests.Shared
{
    public class TestBase
    {
        protected readonly NucleusDbContext DbContext;
        protected readonly IServiceProvider ServiceProvider;

        public TestBase()
        {
            ServiceProvider = TestServer.Host.Services;
            DbContext = ServiceProvider.GetRequiredService<NucleusDbContext>();
            DbContext.Database.EnsureCreated();
        }

        protected IServiceProvider GetNewScopeServiceProvider()
        {
            return ServiceProvider.CreateScope().ServiceProvider;
        }

        protected static ClaimsPrincipal ContextUser => new ClaimsPrincipal(
            new ClaimsIdentity(
                new List<Claim>
                {
                    new Claim(ClaimTypes.Name,  DefaultUsers.Member.UserName)
                },
                "TestAuthenticationType"
            )
        );

        protected static TestServer TestServer => new TestServer(
            new WebHostBuilder()
                .UseStartup<Startup>()
                .UseEnvironment("Test")
                .ConfigureServices(services =>
                {
                    services.AddDbContext<NucleusDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("NucleusTestDb")
                            .UseLazyLoadingProxies()
                            .EnableSensitiveDataLogging();
                    });
                })
                .ConfigureAppConfiguration(config =>
                {
                    config.SetBasePath(Path.Combine(Path.GetFullPath(@"../../../.."), "Nucleus.Tests.Shared"));
                    config.AddJsonFile("appsettings.json", false);
                })
        );

        protected async Task<User> CreateAndGetTestUserAsync()
        {
            var testUser = new User
            {
                Id = Guid.NewGuid(),
                UserName = "TestUserName_" + Guid.NewGuid(),
                Email = "TestUserEmail_" + Guid.NewGuid(),
                PasswordHash = "AM4OLBpptxBYmM79lGOX9egzZk3vIQU3d/gFCJzaBjAPXzYIK3tQ2N7X4fcrHtElTw==", //123qwe
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            testUser.NormalizedEmail = testUser.Email.ToUpper(CultureInfo.GetCultureInfo("en-US"));
            testUser.NormalizedUserName = testUser.UserName.ToUpper(CultureInfo.GetCultureInfo("en-US"));

            await DbContext.Users.AddAsync(testUser);
            await DbContext.UserRoles.AddAsync(new UserRole
            {
                UserId = testUser.Id,
                RoleId = DefaultRoles.Admin.Id
            });
            await DbContext.SaveChangesAsync();

            return testUser;
        }

        protected async Task<Role> CreateAndGetTestRoleAsync()
        {
            var testRole = new Role { Id = Guid.NewGuid(), Name = "TestRoleName_" + Guid.NewGuid() };
            await DbContext.Roles.AddAsync(testRole);
            await DbContext.RolePermissions.AddAsync(new RolePermission
            {
                RoleId = testRole.Id,
                PermissionId = DefaultPermissions.AdministrationAccess.Id
            });

            await DbContext.SaveChangesAsync();
            return testRole;
        }
    }
}
