using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Paymentsense.Coding.Challenge.Contracts.Queries;
using Paymentsense.Coding.Challenge.Contracts.Response;
using Paymentsense.Coding.Challenge.Core.Handlers;
using Paymentsense.Coding.Challenge.Core.Interfaces;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Paymentsense.Coding.Challenge.Api.Extensions
{
    internal static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            services.AddTransient<IRequestHandler<GetCountriesQuery, GetCountriesResponse>, GetCountriesHandler>();

            return services;
        }

        public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Paymentsense",
                    Version = "v1"
                });

                try
                {
                    var basePath = AppContext.BaseDirectory;

                    var directoryInfo = new DirectoryInfo(basePath).GetFiles("Paymentsense.*.xml").Length > 0
                        ? new DirectoryInfo(basePath)
                        : new DirectoryInfo(Path.Combine(basePath, @"bin\Debug\netcoreapp3.1"));

                    foreach (var item in directoryInfo.EnumerateFiles("Paymentsense.*.xml"))
                    {
                        c.IncludeXmlComments(item.FullName);
                    }
                }
                catch (Exception ex)
                {
                }
            });

            return services;
        }
    }
}
