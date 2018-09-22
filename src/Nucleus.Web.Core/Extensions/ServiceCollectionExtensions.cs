using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nucleus.Core.Permissions;
using Nucleus.Core.Roles;
using Nucleus.Core.Users;
using Nucleus.EntityFramework;
using Nucleus.Web.Core.ActionFilters;
using Nucleus.Web.Core.Authentication;

namespace Nucleus.Web.Core.Extensions
{
    public static class ServiceCollection
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });
        }

        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<NucleusDbContext>()
                .AddDefaultTokenProviders();

            JwtTokenAuthConfigurer.Configure(services, configuration);

            services.AddAuthorization(options =>
            {
                foreach (var permission in DefaultPermissions.All())
                {
                    options.AddPolicy(permission.Name,
                        policy => policy.Requirements.Add(new PermissionRequirement(permission)));
                }
            });
        }

        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<NucleusDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                    .UseLazyLoadingProxies());
        }

        public static void ConfigureDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IAuthorizationHandler, PermissionHandler>();
            services.AddScoped<UnitOfWorkActionFilter>();
        }
    }
}
