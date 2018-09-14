using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Nucleus.Application.Permissions;
using Nucleus.Application.Users;

namespace Nucleus.Application
{
    public static class ApplicationServiceCollectionExtensions
    {
        public static IServiceCollection AddNucleusApplication(this IServiceCollection services)
        {
            services.AddAutoMapper();

            ////todo: add conventional registrar
            services.AddTransient<IUserAppService, UserAppService>();
            services.AddTransient<IPermissionAppService, PermissionAppService>();

            return services;
        }
    }
}