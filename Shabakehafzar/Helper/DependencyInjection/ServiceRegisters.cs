using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Shabakehafzar.API.Base.Middleware;
using Shabakehafzar.API.Helper.Config;
using Shabakehafzar.Application.Service.Authentication;
using Shabakehafzar.Application.Service.Persons;
using Shabakehafzar.Core.Models;
using Shabakehafzar.Data.Context;
using Shabakehafzar.Data.UnitOfWork;
using System.Text;

namespace Shabakehafzar.API.Helper.DependencyInjection
{
    public static class ServiceRegisters
    {

        public static void RegisterAllServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationservice, Authenticationservice>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void RegisterAuthentication(this IServiceCollection services, ConfigurationManager configuration)
        {
            var jwtIssuerOptions = new JwtIssuerOptions();
            configuration.GetSection("JwtIssuerOptions").Bind(jwtIssuerOptions);

            SymmetricSecurityKey signingKey =
                new SymmetricSecurityKey(
                    Encoding.ASCII.GetBytes(jwtIssuerOptions.SecretKey));

            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtIssuerOptions.Issuer;
                options.Audience = jwtIssuerOptions.Audience;
                options.SecretKey = jwtIssuerOptions.SecretKey;
                options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtIssuerOptions.Issuer,

                ValidateAudience = true,
                ValidAudience = jwtIssuerOptions.Audience,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtIssuerOptions.Issuer;
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
            });
        }

        public static void RegisterIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, AppRole>(options =>
            {
                // User settings.
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@";
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 9;
                // Lockout settings.
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
            }).AddEntityFrameworkStores<AppDataContext>()
               .AddDefaultTokenProviders();
        }

        public static void RegisterSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "My API",
                    Version = "v1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
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
        }
    }
}
