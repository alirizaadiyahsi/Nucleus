using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Nucleus.Application.Permissions;
using Nucleus.Application.Roles;
using Nucleus.Application.Users;

namespace Nucleus.Application
{
    public static class ApplicationServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureNucleusApplication(this IServiceCollection services)
        {
            services.AddAutoMapper();

            ////todo: add conventional registrar
            services.AddTransient<IUserAppService, UserAppService>();
            services.AddTransient<IPermissionAppService, PermissionAppService>();
            services.AddTransient<IRoleAppService, RoleAppService>();

            return services;
        }
    }
}