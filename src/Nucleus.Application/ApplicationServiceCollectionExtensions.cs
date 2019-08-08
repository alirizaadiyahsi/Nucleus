using System.Reflection;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Nucleus.Utilities.Extensions.Collections;

namespace Nucleus.Application
{
    public static class ApplicationServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureNucleusApplication(this IServiceCollection services)
        {
            // https://github.com/AutoMapper/AutoMapper.Extensions.Microsoft.DependencyInjection/issues/28#issuecomment-339772823
            services.AddAutoMapper(typeof(ApplicationServiceCollectionExtensions));

            services.RegisterAssemblyPublicNonGenericClasses(Assembly.GetExecutingAssembly())
                .Where(c => c.Name.EndsWith("AppService"))
                .AsPublicImplementedInterfaces();

            return services;
        }
    }
}