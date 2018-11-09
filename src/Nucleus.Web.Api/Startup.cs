using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nucleus.Application;
using Nucleus.EntityFramework;
using Nucleus.Web.Core.ActionFilters;
using Nucleus.Web.Core.Extensions;
using Serilog;
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

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureDbContext(_configuration);
            services.ConfigureAuthentication(_configuration);
            services.ConfigureCors(_configuration);
            services.ConfigureDependencyInjection();
            services.ConfigureNucleusApplication();

            services.AddMvc(setup =>
            {
                setup.Filters.AddService<UnitOfWorkActionFilter>();
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Nucleus API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            Log.Fatal(env.EnvironmentName);
            if (env.IsDevelopment())
            {
                //UpdateDatabase(app);
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Nucleus API V1");
            });

            app.UseCors(_configuration["App:CorsOriginPolicyName"]);
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }

        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<NucleusDbContext>())
                {
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();
                }
            }
        }
    }
}
