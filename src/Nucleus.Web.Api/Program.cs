using System.Globalization;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Nucleus.Application;
using Nucleus.Application.Email;
using Nucleus.DataAccess;
using Nucleus.DataAccess.Helpers;
using Nucleus.Domain.AppConstants;
using Nucleus.Domain.Entities.Authorization;
using Nucleus.Web.Core;
using Nucleus.Web.Core.ActionFilters;
using Nucleus.Web.Core.Authorization;
using Nucleus.Web.Core.Configuration;
using Nucleus.Web.Core.Extensions;

var builder = WebApplication.CreateBuilder(args);

#region Service registration

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection(AppConfig.Email_Smtp));
builder.Services.AddSingleton<IEmailSender, EmailSender>();

builder.Services.AddDbContext<NucleusDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString(AppConfig.DefaultConnection))
        .UseLazyLoadingProxies());

builder.Services.AddIdentity<User, Role>(options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequiredLength = 6;
        options.Password.RequiredUniqueChars = 0;
    })
    .AddEntityFrameworkStores<NucleusDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo {Title = "Demo API", Version = "v1"});
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});


var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration[AppConfig.Authentication_JwtBearer_SecurityKey]));
var jwtTokenConfiguration = new JwtTokenConfiguration
{
    Issuer = builder.Configuration[AppConfig.Authentication_JwtBearer_Issuer],
    Audience = builder.Configuration[AppConfig.Authentication_JwtBearer_Audience],
    SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
    StartDate = DateTime.UtcNow,
    EndDate = DateTime.UtcNow.AddDays(60),
};

builder.Services.Configure<JwtTokenConfiguration>(config =>
{
    config.Audience = jwtTokenConfiguration.Audience;
    config.EndDate = jwtTokenConfiguration.EndDate;
    config.Issuer = jwtTokenConfiguration.Issuer;
    config.StartDate = jwtTokenConfiguration.StartDate;
    config.SigningCredentials = jwtTokenConfiguration.SigningCredentials;
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwtBearerOptions =>
{
    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateActor = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtTokenConfiguration.Issuer,
        ValidAudience = jwtTokenConfiguration.Audience,
        IssuerSigningKey = signingKey
    };
});

builder.Services.AddHttpContextAccessor();
builder.Services.ConfigureApplicationServices();
builder.Services.AddScoped<IAuthorizationHandler, PermissionHandler>();
builder.Services.AddAuthorization(options =>
{
    foreach (var permission in AppPermissions.GetAll())
    {
        options.AddPolicy(permission,
            policy => policy.Requirements.Add(new PermissionRequirement(permission)));
    }
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(builder.Configuration[AppConfig.App_CorsOriginPolicyName],
        corsPolicyBuilder =>
            corsPolicyBuilder.WithOrigins(builder.Configuration[AppConfig.App_CorsOrigins]
                    .Split(",", StringSplitOptions.RemoveEmptyEntries))
                .AllowAnyHeader()
                .AllowAnyMethod());
});

builder.Services.AddLocalization();
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    CultureInfo[] supportedCultures =
    {
        new CultureInfo("en"),
        new CultureInfo("tr")
    };

    options.DefaultRequestCulture = new RequestCulture("tr");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
    options.RequestCultureProviders = new List<IRequestCultureProvider>
    {
        new QueryStringRequestCultureProvider(),
        new CookieRequestCultureProvider(),
        new AcceptLanguageHeaderRequestCultureProvider()
    };
});

builder.Services.AddScoped<UnitOfWorkActionFilter>();
var mvcBuilder = builder.Services.AddControllers(options => { options.Filters.AddService<UnitOfWorkActionFilter>(); });
LoadModules(mvcBuilder);

#endregion

#region Middlewares

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<NucleusDbContext>();
    new DbContextDataBuilderHelper(dbContext).SeedData();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder.Configuration[AppConfig.App_CorsOriginPolicyName]);
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.ConfigureCustomExceptionMiddleware();
app.UseRequestLocalization();
app.MapControllers();

app.Run();

#endregion

#region private methods

// TODO: auto load modules instead of hard coding and manual reference
void LoadModules(IMvcBuilder mvcBuilder)
{
    var moduleAssemblies = AppDomain.CurrentDomain.GetAssemblies()
        .Where(a =>
        {
            var name = a.GetName().Name;
            return name != null && name.Contains("Nucleus.Modules");
        });

    foreach (var moduleAssembly in moduleAssemblies)
    {
        mvcBuilder.AddMvcOptions(options => { options.Conventions.Add(new ModuleRoutingConvention(moduleAssembly.Modules)); });
        mvcBuilder.AddApplicationPart(moduleAssembly);
    }
}

#endregion