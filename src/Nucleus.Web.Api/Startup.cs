using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Nucleus.Application;
using Nucleus.Core.Permissions;
using Nucleus.Core.Roles;
using Nucleus.Core.Users;
using Nucleus.EntityFramework;
using Nucleus.Web.Core.ActionFilters;
using Nucleus.Web.Core.Authentication;
using Swashbuckle.AspNetCore.Swagger;

namespace Nucleus.Web.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;


        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<NucleusDbContext>(options =>
                options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"))
                    .UseLazyLoadingProxies());

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<NucleusDbContext>()
                .AddDefaultTokenProviders();

            JwtTokenAuthConfigurer.Configure(services, _configuration);

            services.AddAuthorization(options =>
            {
                foreach (var permission in DefaultPermissions.All())
                {
                    options.AddPolicy(permission.Name,
                        policy => policy.Requirements.Add(new PermissionRequirement(permission)));
                }
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddCors();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Nucleus API", Version = "v1" });
            });
            services.AddNucleusApplication();
            services.AddScoped<IAuthorizationHandler, PermissionHandler>();
            services.AddScoped<UnitOfWorkActionFilter>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Kodkod API V1");
            });

            app.UseCors(builder =>
                builder.WithOrigins(_configuration["App:CorsOrigins"]
                        .Split(",", StringSplitOptions.RemoveEmptyEntries))
                    .AllowAnyHeader()
                    .AllowAnyMethod());

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
