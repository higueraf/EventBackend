using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace EVENT.Api.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            var openApi = new OpenApiInfo
            {
                Title = "Events API",
                Version = "v1",
                Description = "Events Control 2024",
                TermsOfService = new Uri("https://opensource.org/license/gpl-3-0"),
                Contact = new OpenApiContact
                {
                    Name = "Francisco Higuera",
                    Email = "franciscohiguera@gmail.com",
                    Url = new Uri("https://franciscohiguera.info")
                },
                License = new OpenApiLicense
                {
                    Name = "Open Source License",
                    Url = new Uri("https://opensource.org/license/gpl-3-0"),
                }
            };
            services.AddSwaggerGen(s =>
            {
                openApi.Version = "v1";
                s.SwaggerDoc("v1", openApi);
                var securitySchema = new OpenApiSecurityScheme
                {
                    Name = "Jw Authentication",
                    Description = "JWT Bearer Token",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme,
                    }
                };
                s.AddSecurityDefinition(securitySchema.Reference.Id, securitySchema);
                s.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { securitySchema, new string[]{ } }
                });
            });
            return services;
        }
    }
}
