using System;
using System.IO;
using System.Linq;
using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace WalletTrasanction.API
{
    public static class DependencyInjection
    {
        public static void AddWalletTransactionServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
        public class SwaggerOptions
        {
            public string Title { get; set; }
            public string Version { get; set; }
        }
        public static IServiceCollection AddSwaggerService(this IServiceCollection services, IConfiguration configuration)
        {
            SwaggerOptions options = new SwaggerOptions
            {
                Title = configuration["Swagger:Title"],
                Version = configuration["Swagger:Version"]
            };

            
            services.AddSwaggerGen(c =>
            {
               
                c.SwaggerDoc("v1", new OpenApiInfo { Title = $"{options.Title} API", Version = $"{options.Version}" });
                c.IncludeXmlComments(Path.ChangeExtension(Assembly.GetEntryAssembly().Location, "xml"));
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                //c.CustomSchemaIds(x => x.FullName);
                c.CustomSchemaIds(x => DefaultSchemaIdSelector(x));
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
            return services;
        }

        private static string DefaultSchemaIdSelector(Type modelType)
        {
            if (!modelType.IsConstructedGenericType) return modelType.FullName;

            var prefix = modelType.GetGenericArguments()
                .Select(genericArg => DefaultSchemaIdSelector(genericArg))
                .Aggregate((previous, current) => previous + current);

            return prefix + modelType.Name.Split('`').First();
        }


        public static IApplicationBuilder UseSwaggerService(this IApplicationBuilder app,
            IConfiguration configuration, IHostEnvironment environment)
        {
              app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    string swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
                    c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1/swagger.json",
                        $"{configuration["Swagger:Title"]} API V1");
                });
            
            return app;
        }
    }
}
