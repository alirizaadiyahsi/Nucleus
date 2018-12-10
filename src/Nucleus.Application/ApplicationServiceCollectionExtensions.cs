﻿using AutoMapper;
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
            // https://github.com/AutoMapper/AutoMapper.Extensions.Microsoft.DependencyInjection/issues/28#issuecomment-339772823
            services.AddAutoMapper(typeof(ApplicationServiceCollectionExtensions));

            ////todo: add conventional registrar
            services.AddTransient<IUserAppService, UserAppService>();
            services.AddTransient<IPermissionAppService, PermissionAppService>();
            services.AddTransient<IRoleAppService, RoleAppService>();

            return services;
        }
    }
}