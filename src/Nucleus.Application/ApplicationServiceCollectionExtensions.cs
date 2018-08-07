using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Nucleus.Application
{
    public static class ApplicationServiceCollectionExtensions
    {
        public static IServiceCollection AddNucleusApplication(this IServiceCollection services)
        {
            services.AddAutoMapper();

            ////todo: add conventional registrar
            //services.AddTransient<IUserAppService, UserAppService>();
            //services.AddTransient<IPermissionAppService, PermissionAppService>();

            return services;
        }
    }
}
